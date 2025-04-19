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
        [SerializeField] private Button menuButton;
        
        public GameObject RootObject => rootObject;
        public Button MenuButton => menuButton;

        // private void Start()
        // {
        //     rootObject.SetActive(false);
        // }
    }
}