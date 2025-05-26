using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Triangulation3d.Samples.UI;
using UnityEngine;
using VContainer.Unity;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Triangulation3d.Samples
{
    public class MenuPresenter : IAsyncStartable
    {
        private readonly CompositeDisposable disposable = new();
        private readonly List<CancellationTokenSource> cancellationTokenSources = new();
        private readonly MenuModel model;
        private readonly MenuView view;

        private Dictionary<MenuElementType, MenuElementView> cachedElementViews = new();

        private ISynchronizedView<SelectableObjectModel, SelectableObjectView> cachedSelectableObjectView;

        private bool isInitialize = false;

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
                // await UniTask.WaitUntil(() => isInitialize, cancellationToken: cancellationToken);
                //
                // while (!cancellationToken.IsCancellationRequested)
                // {
                //     OnMenuElements();
                //     await UniTask.DelayFrame(2, cancellationToken: cancellationToken);
                // }
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

            model.IsCompletedProperty
                .Subscribe(isCompleted => isInitialize = isCompleted)
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
            var menuElementViewId = 0;

            menuElementModel.MenuElementTypeProperty
                .Subscribe(menuElementType =>
                {
                    menuElementViewId = (int)menuElementType;

                    OnMenuElement(
                        menuElementModel,
                        view.MenuElementViews[menuElementViewId],
                        menuElementType);
                })
                .AddTo(disposable);

            menuElementModel.TextProperty
                .Subscribe(text => view.MenuElementViews[menuElementViewId].Title.text = text)
                .AddTo(view.MenuElementViews[menuElementViewId]);

            menuElementModel.DescriptionProperty
                .Subscribe(description => view.MenuElementViews[menuElementViewId].GetDescriptionObject().SetActive(description))
                .AddTo(view.MenuElementViews[menuElementViewId]);
        }

        /// <summary>
        /// 各メニュー項目の監視
        /// </summary>
        private void OnMenuElement(
            MenuElementModel menuElementModel,
            MenuElementView menuElementView,
            MenuElementType menuElementType)
        {
            switch (menuElementType)
            {
                case MenuElementType.CameraControls:
                    break;
                case MenuElementType.CameraSensitivity:
                    view.CameraSensitivityView.CameraParameterView
                        .Slider
                        .OnValueChangedAsObservable()
                        .Subscribe(value => OnValueChangedAsync(
                            menuElementModel,
                            menuElementView,
                            menuElementType,
                            value).Forget(Debug.LogWarning))
                        .AddTo(disposable);
                    break;
                case MenuElementType.Appearance:
                    view.AppearanceView
                        .ColorParameterView
                        .Button
                        .OnClickAsObservable()
                        .Subscribe(_ =>
                        {
                            // OnClickElementAsync(
                            //     menuElementModel,
                            //     menuElementView,
                            //     menuElementType).Forget(Debug.LogWarning);
                        })
                        .AddTo(disposable);
                    break;
                case MenuElementType.JsonFileUpload:
                    break;
                case MenuElementType.SelectObject:

                    cachedSelectableObjectView = model.SelectObjectModel.ObservableCollection.CreateView(
                        selectableObjectModel =>
                    {
                        var selectableObjectView = Object.Instantiate(
                            view.SelectObjectView.SelectableObjectView,
                            view.SelectObjectView.RootObject.transform);

                        selectableObjectView.Initialize(selectableObjectModel.ObjectName);

                        selectableObjectView.Button.OnClickAsObservable()
                            .Subscribe(_ => OnClickSelectableObjectAsync(selectableObjectView.ObjectName).Forget(Debug.LogWarning))
                            .AddTo(disposable);

                        return selectableObjectView;
                    });

                    break;
            }
        }

        private async UniTask OnValueChangedAsync(
            MenuElementModel menuElementModel,
            MenuElementView menuElementView,
            MenuElementType elementType,
            float rotationSpeed)
        {
            var source = new CancellationTokenSource();
            cancellationTokenSources.Add(source);

            try
            {
                await model.OnValueChangedAsync(
                    menuElementModel,
                    menuElementView,
                    elementType,
                    rotationSpeed,
                    source.Token);
            }
            catch (Exception e)
            {
                source.Cancel();
                Debug.LogWarning(e);
            }
        }

        private async UniTask OnClickElementAsync(
            MenuElementModel menuElementModel,
            MenuElementView menuElementView,
            MenuElementType elementType)
        {
            var source = new CancellationTokenSource();
            cancellationTokenSources.Add(source);

            try
            {
                //var view = cachedElementViews[MenuElementType.CameraSensitivity].Content.CameraParameterView;
                // menuElementView.AppearanceViewTemplate
                //     .ColorParameterView
                //     .Button.interactable = false;
                Debug.Log(elementType);
                await model.ClickAsync(
                    menuElementModel,
                    menuElementView,
                    elementType,
                    source.Token);
            }
            catch (Exception e)
            {
                source.Cancel();
                Debug.LogWarning(e);
            }
        }

        private async UniTask OnClickSelectableObjectAsync(
            string objectName)
        {
            var source = new CancellationTokenSource();
            cancellationTokenSources.Add(source);

            try
            {
                await model.OnSelectableObjectAsync(objectName, source.Token);
            }
            catch (Exception e)
            {

                Debug.LogWarning(e);
            }
        }
    }
}