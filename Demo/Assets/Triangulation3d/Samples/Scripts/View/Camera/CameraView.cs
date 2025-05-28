using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Triangulation3d.Samples
{
    public class CameraView : MonoBehaviour
    {
        private readonly Subject<KeyCode> keyBoardInputSubject = new();
        private readonly Subject<Vector2> mouseInputSubject = new();
        private readonly Subject<float> mouseWheelInputSubject = new();
        
        [SerializeField] private Camera camera;
        [SerializeField] private GameObject target;
        
        /// <summary>
        /// キーボード入力のObservable
        /// </summary>
        public Observable<KeyCode> OnKeyBoardInputAsObservable()
            => keyBoardInputSubject;
        
        /// <summary>
        /// マウス移動
        /// </summary>
        public Observable<Vector2> OnMouseHoverAsObservable()
            => mouseInputSubject;

        public Observable<float> OnMouseWheelInputAsObservable()
            => mouseWheelInputSubject;

        /// <summary>
        /// カメラ
        /// </summary>
        public Camera Camera 
            => camera;
        
        /// <summary>
        /// 観察対象オブジェクト
        /// </summary>
        public GameObject Target
            => target;

        private void Update()
        {
            if (Input.anyKey)
            {
                foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(keyCode))
                    {
                        keyBoardInputSubject.OnNext(keyCode);
                    }
                }
            }
            
            // メニュー画面にカーソルがあるときはズームできないようにする
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                var scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll != 0)
                {
                    mouseWheelInputSubject.OnNext(scroll);
                }
            }
        }
    }

}