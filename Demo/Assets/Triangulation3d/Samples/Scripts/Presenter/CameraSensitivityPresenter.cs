using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Collections.Generic;
using System.Threading;
using VContainer.Unity;
using Debug = UnityEngine.Debug;

namespace Triangulation3d.Samples
{
    public class CameraSensitivityPresenter : IAsyncStartable
    {
        private readonly CameraSensitivityModel model;
        private readonly CameraSensitivityView view;
        
        private readonly CompositeDisposable disposable = new();
        private readonly List<CancellationTokenSource> cancellationTokenSources = new();
        
        public CameraSensitivityPresenter(
            CameraSensitivityModel model, 
            CameraSensitivityView view)
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
        
        void OnSubscribe()
        {
            view.SliderValueProperty
                .Subscribe(value => OnValueChangedAsync(value)
                    .Forget(Debug.LogWarning))
                .AddTo(disposable);
        }

        private async UniTask OnValueChangedAsync(float rotationSpeed)
        {
            var source = new CancellationTokenSource();
            cancellationTokenSources.Add(source);
            
            try
            {
                await model.OnRotationSpeedChangedAsync(
                    rotationSpeed,
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