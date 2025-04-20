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
        
        private readonly CameraSensitivityModel cameraSensitivityModel;

        public MenuModel(CameraSensitivityModel cameraSensitivityModel)
        {
            this.cameraSensitivityModel = cameraSensitivityModel;
        }

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
                            "Camera Controls", 
                            OnClickCameraSensitivity, 
                            menuElementType,
                            true));
                        break;
                    case MenuElementType.CameraSensitivity:
                        AddElement(new MenuElementModel(
                            "Camera Sensitivity", 
                            OnClickCameraSensitivity, 
                            menuElementType));
                        break;
                    case MenuElementType.Appearance:
                        AddElement(new MenuElementModel(
                            "Appearance", 
                            OnClickCameraSensitivity, 
                            menuElementType));
                        break;
                    case MenuElementType.JsonFileUpload:
                        AddElement(new MenuElementModel(
                            "Json File Upload", 
                            OnClickCameraSensitivity, 
                            menuElementType));
                        break;
                    case MenuElementType.SelectObject:
                        AddElement(new MenuElementModel(
                            "Select Object", 
                            OnClickCameraSensitivity, 
                            menuElementType));
                        break;
                }
            }
            
        }

        public void AddElement(MenuElementModel menuElementModel)
        {
            addElementSubject.OnNext(menuElementModel);
        }
        
        public void AddElementType(MenuElementType menuElementType)
        {
            
        }

        public async UniTask OnClickCameraSensitivity(CancellationToken cancellationToken)
        {

        }

        
    }
}