using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

namespace Triangulation3d.Samples
{
    public class MeshSamplesPresenter : IAsyncStartable
    {
        private readonly MeshSamplesModel model;
		private readonly MeshSamplesView view;

        public MeshSamplesPresenter(
            MeshSamplesModel model,
            MeshSamplesView view)
        {
            this.model = model;
            this.view = view;
            OnSubscribe();
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await model.StartAsync(cancellationToken);
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