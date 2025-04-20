using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using R3;
using System;
using System.Collections.Generic;
using System.Threading;
using Triangulation3d.Samples.UI;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Triangulation3d.Samples
{
    public class MenuPresenter : IAsyncStartable
    {
        private readonly CompositeDisposable disposable = new();
        private readonly MenuModel model;
        private readonly MenuView view;
        
        private Dictionary<MenuElementType, MenuElementView> elementViews = new();

        public MenuPresenter(MenuModel model, MenuView view)
        {
            this.model = model;
            this.view = view;
            
            //CreateMenu(view.ContentObject.transform);
            OnSubscribe();
        }
        
        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                //await model.StartAsync(cancellationToken);
                model.InitializeElements();
                
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        private void OnSubscribe()
        {
            model.OnElementAddedAsObservable()
                .Subscribe(OnElementAdded)
                .AddTo(disposable);
            
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
            view.MenuButton.transform.Rotate(Vector3.forward, 180f);
            model.IsVisibleProperty.OnNext(!model.IsVisibleProperty.Value);
        }

        private void OnIsVisible(bool isVisible)
        {
            view.RootObject.SetActive(isVisible);
        }

        private void OnElementAdded(MenuElementModel menuElementModel)
        {
            var elementView = Object.Instantiate(view.MenuElementViewTemplate, view.ContentObject.transform);
            
            menuElementModel.TextProperty
                .Subscribe(text => elementView.Title.text = text)
                .AddTo(elementView);
            
            menuElementModel.DescriptionProperty
                .Subscribe(description => elementView.GetDescriptionObject().SetActive(description))
                .AddTo(elementView);
            
            menuElementModel.MenuElementTypeProperty
                .Subscribe(elementType =>
                {
                    OnMenuContent(elementType, elementView);
                })
                .AddTo(elementView);
        }

        private void OnMenuContent(
            MenuElementType menuElementType,
            MenuElementView menuElementView)
        {
            if (elementViews.TryGetValue(menuElementType, out var elementView)) return;
            
            switch (menuElementType)
            {
                case MenuElementType.CameraControls:
                    menuElementView.Content = Object.Instantiate(view.CameraControlsViewTemplate, menuElementView.GetContentObject().transform);
                    menuElementView.MenuElementRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 260);
                    break;
                case MenuElementType.CameraSensitivity:
                    menuElementView.Content = Object.Instantiate(view.CameraSensitivityViewTemplate, menuElementView.GetContentObject().transform);
                    menuElementView.MenuElementRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 150);
                    break;
                case MenuElementType.Appearance:
                    menuElementView.Content = Object.Instantiate(view.AppearanceViewTemplate, menuElementView.GetContentObject().transform);
                    menuElementView.MenuElementRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
                    break;
                case MenuElementType.JsonFileUpload:
                    menuElementView.Content = Object.Instantiate(view.JsonFileUploadViewTemplate, menuElementView.GetContentObject().transform);
                    menuElementView.MenuElementRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);
                    break;
                case MenuElementType.SelectObject:
                    menuElementView.Content = Object.Instantiate(view.SelectObjectViewTemplate, menuElementView.GetContentObject().transform);
                    menuElementView.MenuElementRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);
                    break;
            }
            
            elementViews.Add(menuElementType, menuElementView);
        }
    }
}