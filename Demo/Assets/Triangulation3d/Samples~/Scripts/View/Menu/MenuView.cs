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
        
        /// <summary>
        /// メニューの各項目のUIのテンプレート’
        /// </summary>
        [SerializeField] private CameraControlsView cameraControlsViewTemplate;
        [SerializeField] private CameraSensitivityView cameraSensitivityViewTemplate;
        [SerializeField] private AppearanceView appearanceViewTemplate;
        [SerializeField] private JsonFileUploadView jsonFileUploadViewTemplate;
        [SerializeField] private SelectObjectView selectObjectViewTemplate;
        [SerializeField] private MenuElementView menuElementViewTemplate;
        
        public GameObject RootObject => rootObject;
        public GameObject ContentObject => contentObject;
        public Button MenuButton => menuButton;
        public CameraControlsView CameraControlsViewTemplate => cameraControlsViewTemplate;
        public CameraSensitivityView CameraSensitivityViewTemplate => cameraSensitivityViewTemplate;
        public AppearanceView AppearanceViewTemplate => appearanceViewTemplate;
        public JsonFileUploadView JsonFileUploadViewTemplate => jsonFileUploadViewTemplate;
        public SelectObjectView SelectObjectViewTemplate => selectObjectViewTemplate;
        public MenuElementView MenuElementViewTemplate => menuElementViewTemplate;
    }
}