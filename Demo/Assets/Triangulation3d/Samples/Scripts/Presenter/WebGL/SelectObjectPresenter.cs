using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Collections.Generic;
using System.Threading;
using VContainer.Unity;
using Debug = UnityEngine.Debug;

namespace Triangulation3d.Samples
{
    public class SelectObjectPresenter : IAsyncStartable
    {
        private readonly SelectObjectModel model;
        private readonly SelectObjectView view;
        
        private readonly CompositeDisposable disposable = new();
        private readonly List<CancellationTokenSource> cancellationTokenSources = new();

        public SelectObjectPresenter(SelectObjectModel model, SelectObjectView view)
        {
            this.model = model;
            this.view = view;
            
            OnSubscribe();
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
        
        private void Dispose()
        {
            foreach (var source in cancellationTokenSources)
            {
                source.Cancel();
                source.Dispose();
            }

            disposable.Dispose();
        }
        
        void OnSubscribe()
        {
            view.SelectableObjectNameProperty
                .Subscribe(name => OnSelectableObjectChangedAsync(name)
                    .Forget(Debug.LogWarning))
                .AddTo(view);
        }
        
        private async UniTask OnSelectableObjectChangedAsync(string objectName)
        {
            var source = new CancellationTokenSource();
            cancellationTokenSources.Add(source);
            
            try
            {
                await model.OnSelectableObjectChangedAsync(
                    objectName,
                    source.Token);
            }
            catch (Exception e)
            {
                source.Cancel();
                Debug.LogWarning(e);
            }
        }
    }
}