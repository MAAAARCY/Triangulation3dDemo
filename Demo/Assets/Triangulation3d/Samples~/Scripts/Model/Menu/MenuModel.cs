using Cysharp.Threading.Tasks;
using iShape.Geometry.Polygon;
using R3;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Triangulation3d.Samples.UI;
using UnityEngine;

namespace Triangulation3d.Samples
{
    public class MenuModel
    {
        private List<BaseMenuElementView> menuElementViews;
        private MenuType[] menuTypes;
        
        /// <summary>
        /// メニューを表示するか
        /// </summary>
        public readonly ReactiveProperty<bool> IsVisibleProperty = new(false);
        
        private readonly CameraControlsView cameraControlsViewTemplate; 
        private readonly CameraSensitivityView cameraSensitivityViewTemplate;
        private readonly AppearanceView appearanceViewTemplate;
        private readonly JsonFileUploadView jsonFileUploadViewTemplate;
        private readonly SelectObjectView selectObjectViewTemplate;
        private readonly BaseMenuElementView baseMenuElementViewTemplate;

        public MenuModel(
            CameraControlsView cameraControlsViewTemplate, 
            CameraSensitivityView cameraSensitivityViewTemplate,
            AppearanceView appearanceViewTemplate,
            JsonFileUploadView jsonFileUploadViewTemplate,
            SelectObjectView selectObjectViewTemplate,
            BaseMenuElementView baseMenuElementViewTemplate)
        {
            this.cameraControlsViewTemplate = cameraControlsViewTemplate;
            this.cameraSensitivityViewTemplate = cameraSensitivityViewTemplate;
            this.appearanceViewTemplate = appearanceViewTemplate;
            this.jsonFileUploadViewTemplate = jsonFileUploadViewTemplate;
            this.selectObjectViewTemplate = selectObjectViewTemplate;
            this.baseMenuElementViewTemplate = baseMenuElementViewTemplate;
            
            menuTypes = new MenuType[]
            {
                MenuType.CameraControls, 
                MenuType.CameraSensitivity,
                MenuType.Appearance,
                MenuType.JsonFileUpload,
                MenuType.SelectObject
            };

            menuElementViews = new List<BaseMenuElementView>();
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {

        }

        public void CreateMenu(Transform parent)
        {
            foreach (var menuType in menuTypes)
            {
                var baseElement = Object.Instantiate(baseMenuElementViewTemplate, parent);

                switch (menuType)
                {
                    case MenuType.CameraControls:
                        baseElement.Title.SetText("Controls");
                        baseElement.GetDescriptionObject().SetActive(true);
                        baseElement.Content = Object.Instantiate(
                            cameraControlsViewTemplate, 
                            baseElement.GetContentObject().transform);
                        break;
                    case MenuType.CameraSensitivity:
                        baseElement.Title.SetText("Camera Sensitivity");
                        baseElement.GetDescriptionObject().SetActive(false);
                        baseElement.Content = Object.Instantiate(
                            cameraSensitivityViewTemplate, 
                            baseElement.GetContentObject().transform);
                        break;
                    case MenuType.Appearance:
                        baseElement.Title.SetText("Appearance");
                        baseElement.GetDescriptionObject().SetActive(false);
                        baseElement.Content = Object.Instantiate(
                            appearanceViewTemplate, 
                            baseElement.GetContentObject().transform);
                        break;
                    case MenuType.JsonFileUpload:
                        baseElement.Title.SetText("Json File Upload");
                        baseElement.GetDescriptionObject().SetActive(false);
                        baseElement.Content = Object.Instantiate(
                            jsonFileUploadViewTemplate, 
                            baseElement.GetContentObject().transform);
                        break;
                    case MenuType.SelectObject:
                        baseElement.Title.SetText("Select Object");
                        baseElement.GetDescriptionObject().SetActive(false);
                        baseElement.Content = Object.Instantiate(
                            selectObjectViewTemplate, 
                            baseElement.GetContentObject().transform);
                        break;
                    default:
                        break;
                }
                
                menuElementViews.Append(baseElement);
            }
        }
    }
}