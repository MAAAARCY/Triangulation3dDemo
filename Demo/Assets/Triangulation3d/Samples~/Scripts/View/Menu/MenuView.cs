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
        
        public GameObject RootObject => rootObject;
        public GameObject ContentObject => contentObject;
        public Button MenuButton => menuButton;
    }
}