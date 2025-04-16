using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// カメラの自己位置を計算するモデル
    /// </summary>
    public class CameraPoseCalculatorModel
    {
        private readonly float moveSpeed = 0.1f;
        private readonly float rotationSpeed = 0.5f;
        private readonly float smoothTime = 0.01f;
        
        /// <summary>
        /// ターゲットオブジェクトを円の中心としたカメラの角度
        /// </summary>
        private float currentAngle = 0;
        
        /// <summary>
        /// Mathf.Abs(観察対象のオブジェクトのPosition - カメラのPosition)
        /// </summary>
        private float radius;
        
        public void InitializePose(Camera camera)
        {
            currentAngle = CalculateInitialAngle(camera);
            radius = Mathf.Abs(camera.transform.position.z - Vector3.zero.z); //TODO:Vector3.zeroを観察対象オブジェクトのTransformに変更
        }
        
        /// <summary>
        /// カメラの初期位置がVector3.zero以外に居る時に、補正する用の角度
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public float CalculateInitialAngle(Camera camera)
        {
            var from = camera.transform.position;
            var to = Vector3.right;
            
            var projection = Vector3.ProjectOnPlane(from, Vector3.up);
            var result = Vector3.SignedAngle(projection, to, Vector3.up);

            return result;
        }
        
        public Vector3 CalculateCameraPose(KeyCode keyCode, Camera camera)
        {
            switch (keyCode)
            {
                case KeyCode.A:
                    return CalculateCameraPoseInCircle(camera, rotationSpeed);
                // case KeyCode.S:
                //     result += Vector3.back * moveSpeed;
                //     break;
                case KeyCode.D:
                    return CalculateCameraPoseInCircle(camera, -rotationSpeed);
                // case KeyCode.W:
                //     result += Vector3.forward * moveSpeed;
                //     break;
                default:
                    return camera.transform.position;
            }
        }

        private Vector3 CalculateCameraPoseInCircle(Camera camera, float speed)
        {
            currentAngle += speed;
                        
            if (currentAngle >= 360.0f)
            {
                currentAngle -= 360.0f;
            }
                    
            // 角度をラジアンに変換
            var radians = currentAngle * Mathf.Deg2Rad;
        
            // 円周上の位置を計算
            // TODO: Vector3.zeroをターゲットのTransformに変更
            var x = Vector3.zero.x + radius * Mathf.Cos(radians);
            var z = Vector3.zero.z + radius * Mathf.Sin(radians);
        
            // カメラの位置を設定
            var result = new Vector3(x, 0, z); // TODO:Yも動かせるようにする

            return result;
        }
    }
}