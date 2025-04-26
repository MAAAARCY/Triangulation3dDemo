using Cysharp.Threading.Tasks;
using iShape.Geometry.Polygon;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Triangulation3d.Samples.UI;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// Menu全体のモデル
    /// </summary>
    public class MenuModel
    {
        private List<MenuElementView> menuElementViews;
        private MenuElementType[] menuTypes;
        private Dictionary<MenuElementType, String> menuTypeNames;
        private readonly Subject<MenuElementModel> addElementSubject = new();
        private readonly Subject<MenuElementType> addElementTypeSubject = new();
        
        /// <summary>
        /// メニューの要素を追加のObservable
        /// </summary>
        public Observable<MenuElementModel> OnElementAddedAsObservable()
            => addElementSubject;
        
        /// <summary>
        /// メニューの要素を追加のObservable
        /// </summary>
        public Observable<MenuElementType> OnElementTypeAddedAsObservable()
            => addElementTypeSubject;
        
        /// <summary>
        /// メニューを表示するか
        /// </summary>
        public readonly ReactiveProperty<bool> IsVisibleProperty = new(false);
        
        /// <summary>
        /// メニューの初期化が正常に終了したかどうか
        /// </summary>
        public readonly ReactiveProperty<bool> IsCompletedProperty = new(false);
        
        private readonly CameraSensitivityModel cameraSensitivityModel;
        private readonly ApperanceModel appearanceModel;
        private readonly JsonFileUploadModel jsonFileUploadModel;
        private readonly SelectObjectModel selectObjectModel;
        
        public SelectObjectModel SelectObjectModel => selectObjectModel;

        public MenuModel(
            CameraSensitivityModel cameraSensitivityModel,
            ApperanceModel appearanceModel,
            JsonFileUploadModel jsonFileUploadModel,
            SelectObjectModel selectObjectModel)
        {
            this.cameraSensitivityModel = cameraSensitivityModel;
            this.appearanceModel = appearanceModel;
            this.jsonFileUploadModel = jsonFileUploadModel;
            this.selectObjectModel = selectObjectModel;
            
            //InitializeElements();
        }
        
        /// <summary>
        /// 各MenuElementの初期化
        /// </summary>
        public void InitializeElements()
        {
            var menuElementTypes = new MenuElementType[]
            {
                MenuElementType.CameraControls, MenuElementType.CameraSensitivity, MenuElementType.Appearance,
                MenuElementType.JsonFileUpload, MenuElementType.SelectObject
            };

            foreach (var menuElementType in menuElementTypes)
            {
                switch (menuElementType)
                {
                    case MenuElementType.CameraControls:
                        AddElement(new MenuElementModel(
                            text: "Camera Controls", 
                            onClickAsync: cameraSensitivityModel.OnClickCameraSensitivityAsync, 
                            elementType: menuElementType,
                            descriptionEnable: true));
                        break;
                    case MenuElementType.CameraSensitivity:
                        AddElement(new MenuElementModel(
                            text: "Camera Sensitivity", 
                            onClickAsync: cameraSensitivityModel.OnClickCameraSensitivityAsync, 
                            elementType: menuElementType));
                        break;
                    case MenuElementType.Appearance:
                        AddElement(new MenuElementModel(
                            text: "Appearance", 
                            onClickAsync: cameraSensitivityModel.OnClickCameraSensitivityAsync, 
                            elementType: menuElementType));
                        break;
                    case MenuElementType.JsonFileUpload:
                        AddElement(new MenuElementModel(
                            text: "Json File Upload\n(Not Implemented)", 
                            onClickAsync: cameraSensitivityModel.OnClickCameraSensitivityAsync, 
                            elementType: menuElementType));
                        break;
                    case MenuElementType.SelectObject:
                        AddElement(new MenuElementModel(
                            text: "Select Object", 
                            onClickAsync: cameraSensitivityModel.OnClickCameraSensitivityAsync, 
                            elementType: menuElementType));
                        selectObjectModel.InitializeSelectableObject();
                        break;
                }
            }
        }
        
        

        public void AddElement(MenuElementModel menuElementModel)
        {
            addElementSubject.OnNext(menuElementModel);
        }

        public async UniTask OnValueChangedAsync(
            MenuElementModel menuElementModel,
            MenuElementView menuElementView,
            MenuElementType menuElementType,
            float value,
            CancellationToken cancellationToken)
        {
            switch (menuElementType)
            {
                case MenuElementType.CameraSensitivity:
                    await cameraSensitivityModel.OnRotationSpeedChangedAsync(value, cancellationToken);
                    break;
                default:
                    await UniTask.Yield();
                    break;
            }
        }

        public async UniTask OnSelectableObjectAsync(
            string objectName,
            CancellationToken cancellationToken)
        {
            await selectObjectModel.OnSelectableObjectChangedAsync(objectName, cancellationToken);
        }
        
        public async UniTask ClickAsync(
            MenuElementModel menuElementModel,
            MenuElementView menuElementView,
            MenuElementType menuElementType,
            CancellationToken cancellationToken)
        {
            switch (menuElementType)
            {
                case MenuElementType.Appearance:
                    break;
                case MenuElementType.JsonFileUpload:
                    break;
                case MenuElementType.SelectObject:
                    break;
            }
        }
    }
}