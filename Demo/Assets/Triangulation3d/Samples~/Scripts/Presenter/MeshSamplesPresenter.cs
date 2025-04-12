using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

namespace Triangulation3d.Samples
{
    public class MeshSamplesPresenter : IAsyncStartable
    {
        private readonly MeshSamplesModel meshSamplesModel;
		private readonly MeshSamplesView meshSamplesView;

        public MeshSamplesPresenter(
            MeshSamplesModel meshSamplesModel,
            MeshSamplesView meshSamplesView)
        {
            this.meshSamplesModel = meshSamplesModel;
            this.meshSamplesView = meshSamplesView;
            OnSubscribe();
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await meshSamplesModel.StartAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        private async UniTask OnSubscribe()
        {
            
        }
    }
}