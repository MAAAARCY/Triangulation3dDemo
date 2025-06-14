using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using System.Collections.Generic;
using Triangulation3d.Runtime;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Triangulation3d.Samples
{
    public class MeshSamplesModel : IDisposable
    {
        private readonly MeshFactoryModel meshFactoryModel;
        private readonly MeshRepository meshRepository;
        private readonly SurfaceRepository surfaceRepository;
        private readonly CombinedMeshRepository combinedMeshRepository;
        private readonly MeshModel meshModel;
        
        [DllImport("__Internal")]
        private static extern void Callback(string message);
        
        public Subject<string> SurfaceAddedSubject
            => surfaceRepository.SurfaceAddedSubject;
        
        public Observable<string> OnObjectAddedAsObservable()
            => combinedMeshRepository.AddedObjectNameProperty.AsObservable();
        
        public MeshSamplesModel(
            MeshFactoryModel meshFactoryModel,
            MeshRepository meshRepository,
            SurfaceRepository surfaceRepository,
            CombinedMeshRepository combinedMeshRepository,
            MeshModel meshModel)
        {
            this.meshFactoryModel = meshFactoryModel;
            this.meshRepository = meshRepository;
            this.surfaceRepository = surfaceRepository;
            this.combinedMeshRepository = combinedMeshRepository;
            this.meshModel = meshModel;
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            // StartAsyncに書くべき処理ではないため移動を検討
            var objectNames = new List<string>() { "Cube", "Suzanne" };
            
            foreach (var objectName in objectNames)
            {
                var surfaces = await GetSurfacesAsync(
                    objectName,
                    cancellationToken:cancellationToken);
                
                surfaceRepository.SetSurfaces(
                    objectName,
                    surfaces);
            }
        }

        private async UniTask<List<Surface>> GetSurfacesAsync(
            string objectName,
            CancellationToken cancellationToken)
        {
            var surfaces = await surfaceRepository.GetSurfacesAsync(
                objectName,
                cancellationToken);

            return surfaces;
        }

        public async UniTask CreateMeshAsync(
            string objectName,
            CancellationToken cancellationToken)
        {
            var surfaces = surfaceRepository.CachedSurfaces[objectName];
            var meshViews = await GetMeshViewsAsync(surfaces, cancellationToken);
            var combinedMeshView = await GetCombinedMeshViewAsync(
                meshViews, 
                objectName,
                cancellationToken);
            
            meshRepository.SetMeshViews(
                objectName,
                meshViews);
            
            combinedMeshRepository.SetCombinedMesh(
                objectName,
                combinedMeshView);
        }

        private async UniTask<List<MeshView>> GetMeshViewsAsync(
            List<Surface> surfaces,
            CancellationToken cancellationToken)
        {
            var meshViews = new List<MeshView>();
            foreach (var surface in surfaces)
            {
                var meshView = await UniTask.Create(() => 
                    meshFactoryModel.CreateMeshView(
                        surface: surface,
                        cancellationToken: cancellationToken));
                
                meshViews.Add(meshView);
            }
            
            return meshViews;
        }
        
        /// <summary>
        /// meshViewsを結合する処理（座標変換が上手く行ってない）
        /// 一旦CombineMeshViewをparentにする処理にしてる
        /// </summary>
        /// <param name="meshViews"></param>
        /// <param name="objectName"></param>>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async UniTask<CombinedMeshView> GetCombinedMeshViewAsync(
            List<MeshView> meshViews,
            string objectName,
            CancellationToken cancellationToken)
        {
            var combinedMeshView = await meshFactoryModel.CreateCombinedMeshView(
                meshViews,
                objectName,
                cancellationToken);
            
            combinedMeshRepository.SetCombinedMesh(objectName, combinedMeshView);
            
            return combinedMeshView;
        }
        
        /// <summary>
        /// 新規オブジェクトの作成が完了したことをReact側に通知
        /// </summary>
        /// <param name="objectName"></param>
        public void OnObjectAdded(string objectName)
        {
            Callback(objectName);
            Debug.Log($"Object added: {objectName}");
        }
        
        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            meshRepository.Clear();
        }
    }
}