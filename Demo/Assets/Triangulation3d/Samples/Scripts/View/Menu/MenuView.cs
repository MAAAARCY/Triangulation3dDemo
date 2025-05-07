using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Triangulation3d.Samples.UI
{
    public class MenuView : MonoBehaviour
    {
        /// <summary>
        /// メニュー全体のUI
        /// </summary>
        [SerializeField] private GameObject rootObject;
        [SerializeField] private GameObject contentObject;
        [SerializeField] private Button menuButton;
        
        [SerializeField] private MenuElementView[] menuElementViews;
        
        /// <summary>
        /// メニューの各項目のUIのテンプレート’
        /// </summary>
        [SerializeField] private CameraControlsView cameraControlsView;
        [SerializeField] private CameraSensitivityView cameraSensitivityView;
        [SerializeField] private AppearanceView appearanceView;
        [SerializeField] private JsonFileUploadView jsonFileUploadView;
        [SerializeField] private SelectObjectView selectObjectView;

        public GameObject RootObject => rootObject;
        public GameObject ContentObject => contentObject;
        public Button MenuButton => menuButton;
        public MenuElementView[] MenuElementViews => menuElementViews;
        public CameraControlsView CameraControlsView => cameraControlsView;
        public CameraSensitivityView CameraSensitivityView => cameraSensitivityView;
        public AppearanceView AppearanceView => appearanceView;
        public JsonFileUploadView JsonFileUploadView => jsonFileUploadView;
        public SelectObjectView SelectObjectView => selectObjectView;
    }
}