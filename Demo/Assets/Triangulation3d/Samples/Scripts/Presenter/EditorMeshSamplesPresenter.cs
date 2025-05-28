using System;
using Cysharp.Threading.Tasks;
using R3;
using System.Collections.Generic;
using System.Threading;
using Triangulation3d.Runtime;
using UnityEngine;
using VContainer.Unity;

namespace Triangulation3d.Samples
{
    public class EditorMeshSamplesPresenter : IAsyncStartable
    {
        private readonly MeshSamplesModel model;
        private readonly MeshSamplesView view;

        private readonly CompositeDisposable disposable = new();
        private readonly List<CancellationTokenSource> cancellationTokenSources = new();

        public EditorMeshSamplesPresenter(
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
        
        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            foreach (var source in cancellationTokenSources)
            {
                source.Cancel();
                source.Dispose();
            }
            
            disposable.Dispose();
        }

        private void OnSubscribe()
        {
            model.SurfaceAddedSubject
                .Subscribe(name => OnSurfaceAdded(name).Forget(Debug.LogWarning))
                .AddTo(disposable);
        }

        private async UniTask OnSurfaceAdded(string name)
        {
            // Debug.Log(name);
            if (name == "Sample") return;
            // Debug.Log(name);
            
            var source = new CancellationTokenSource();
            cancellationTokenSources.Add(source);

            try
            {
                await model.CreateMeshAsync(name, source.Token);
            }
            catch (Exception e)
            {
                source.Cancel();
                Debug.LogWarning(e);
            }
        }
    }
}

