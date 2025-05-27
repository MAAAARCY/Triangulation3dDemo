using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Collections.Generic;
using System.Threading;
using VContainer.Unity;
using Debug = UnityEngine.Debug;

namespace Triangulation3d.Samples
{
    public class JsonFileUploadPresenter : IAsyncStartable
    {
        private readonly JsonFileUploadModel model;
        private readonly JsonFileUploadView view;
        
        // private readonly CompositeDisposable disposable = new();
        private readonly List<CancellationTokenSource> cancellationTokenSources = new();

        public JsonFileUploadPresenter(
            JsonFileUploadModel model, 
            JsonFileUploadView view)
        {
            this.model = model;
            this.view = view;
            
            OnSubscribe();
        }
        
        private void OnSubscribe()
        {
            view.JsonContentProperty
                .Subscribe(jsonContent => OnJsonFileUploadButtonClickedAsync(jsonContent)
                    .Forget(Debug.LogWarning))
                .AddTo(view);
        }
        
        private void Dispose()
        {
            foreach (var source in cancellationTokenSources)
            {
                source.Cancel();
                source.Dispose();
            }
        }
        
        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await UniTask.Yield();
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        private async UniTask OnJsonFileUploadButtonClickedAsync(string jsonContent)
        {
            var source = new CancellationTokenSource();
            cancellationTokenSources.Add(source);
            Debug.Log("OnJsonFileUploadButtonClickedAsync");
            try
            {
                await model.OnJsonFileUploadAsync(jsonContent, source.Token);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
            finally
            {
                source.Dispose();
            }
        }
    }
}