using R3;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Triangulation3d.Samples
{
    public class CameraView : MonoBehaviour
    {
        private readonly Subject<KeyCode> keyBoardInputSubject = new();
        private readonly Subject<Vector2> mouseInputSubject = new();
        
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
        
        // public void Move(Vector3 localMove, float speed)
        // {
        //     camera.transform.position += transform.TransformDirection(localMove) * speed * Time.deltaTime;
        // }
        //
        // public void Rotate(Vector2 rotation)
        // {
        //     camera.transform.rotation = Quaternion.Euler(rotation.y, rotation.x, 0f);
        // }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
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
        }
    }

}