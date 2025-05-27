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
    public class WebGLMeshSamplesPresenter : IAsyncStartable
    {
        private readonly MeshSamplesModel model;
        private readonly MeshSamplesView view;

        private readonly CompositeDisposable disposable = new();
        private readonly List<CancellationTokenSource> cancellationTokenSources = new();

        public WebGLMeshSamplesPresenter(
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
                // await model.StartAsync(cancellationToken);
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
            
            // model.OnSurfaceAddedAsObservable()
            //     .Subscribe(name => OnSurfaceAdded(name).Forget(Debug.LogWarning))
            //     .AddTo(disposable);

            model.OnObjectAddedAsObservable()
                .Subscribe(OnObjectAdded)
                .AddTo(disposable);
        }

        private async UniTask OnSurfaceAdded(string name)
        {
            // Debug.Log(name);
            if (name == "Sample") return;
            Debug.Log($"{name} in OnSurfaceAdded");
            
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

        /// <summary>
        /// 新規オブジェクトの作成が完了したことをReact側に通知
        /// </summary>
        /// <param name="objectName"></param>
        private void OnObjectAdded(string objectName)
        {
            // Debug.Log(objectName);
            if (objectName == "Sample") return;
            Debug.Log($"{objectName} in OnObjectAdded");

        var source = new CancellationTokenSource();
            cancellationTokenSources.Add(source);

            try
            {
                model.OnObjectAdded(objectName);
            }
            catch (Exception e)
            {
                source.Cancel();
                Debug.LogWarning(e);
            }
        }
    }
}
