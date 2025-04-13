using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Triangulation3d.Runtime;
using UnityEngine;

namespace Triangulation3d.Samples
{
    public class MeshSamplesModel : IDisposable
    {
        private readonly MeshFactoryModel meshFactoryModel;
        private readonly MeshRepository meshRepository;
        private readonly SurfaceRepository surfaceRepository;
        private readonly MeshModel meshModel;
        
        public MeshSamplesModel(
            MeshFactoryModel meshFactoryModel,
            MeshRepository meshRepository,
            SurfaceRepository surfaceRepository,
            MeshModel meshModel)
        {
            this.meshFactoryModel = meshFactoryModel;
            this.meshRepository = meshRepository;
            this.surfaceRepository = surfaceRepository;
            this.meshModel = meshModel;
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            await OnMeshesAsync(cancellationToken);
        }

        private async UniTask OnMeshesAsync(CancellationToken cancellationToken)
        {
            var surfaces = await surfaceRepository.GetSurfacesAsync("Assets/Triangulation3d/Samples~/Scripts/JsonFiles/SuzanneVertices.json", cancellationToken);
            var meshViews = await GetMeshViewsAsync(surfaces, cancellationToken);
            
            meshRepository.SetMeshViews(meshViews);
        }

        private async UniTask<List<MeshView>> GetMeshViewsAsync(
            List<Surface> surfaces,
            CancellationToken cancellationToken)
        {
            var meshViews = new List<MeshView>();
            foreach (var surface in surfaces)
            {
                var meshView = await meshFactoryModel.CreateMeshView(
                    surface: surface,
                    cancellationToken: cancellationToken);
                
                meshViews.Add(meshView);
            }
            
            return meshViews;
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