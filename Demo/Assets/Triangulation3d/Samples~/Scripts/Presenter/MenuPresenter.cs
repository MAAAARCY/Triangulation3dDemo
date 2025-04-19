using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using Triangulation3d.Samples.UI;
using UnityEngine;
using VContainer.Unity;

namespace Triangulation3d.Samples
{
    public class MenuPresenter : IAsyncStartable
    {
        private readonly CompositeDisposable disposable = new();
        private readonly MenuModel model;
        private readonly MenuView view;

        public MenuPresenter(MenuModel model, MenuView view)
        {
            this.model = model;
            this.view = view;
            
            model.CreateMenu(view.RootObject.transform);
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

        private void OnSubscribe()
        {
            model.IsVisibleProperty
                .Subscribe(OnIsVisible)
                .AddTo(disposable);
            
            view.MenuButton
                .OnClickAsObservable()
                .Subscribe(_ => OnClickMenu())
                .AddTo(disposable);
        }

        private void OnClickMenu()
        {
            model.IsVisibleProperty.OnNext(!model.IsVisibleProperty.Value);
        }

        private void OnIsVisible(bool isVisible)
        {
            view.RootObject.SetActive(isVisible);
        }
    }
}