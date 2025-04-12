using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Triangulation3d.Samples
{
    public class MeshSamplesModel : IDisposable
    {
        private readonly MeshFactoryModel meshFactoryModel;
        private readonly MeshRepository meshRepository;
        private readonly MeshModel meshModel;
        
        public MeshSamplesModel(
            MeshFactoryModel meshFactoryModel,
            MeshRepository meshRepository,
            MeshModel meshModel)
        {
            this.meshFactoryModel = meshFactoryModel;
            this.meshRepository = meshRepository;
            this.meshModel = meshModel;
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            await OnMeshesAsync(cancellationToken);
        }

        private async UniTask OnMeshesAsync(CancellationToken cancellationToken)
        {
            var meshes = meshRepository.Data3d;
            foreach (var mesh in meshes)
            {
                var meshView = await meshFactoryModel.CreateMeshView(
                    hull: mesh.Hull,
                    holes: mesh.Holes,
                    cancellationToken: cancellationToken);
                
                meshRepository.SetMesh(meshView);
            }
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