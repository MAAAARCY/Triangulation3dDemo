using UnityEngine;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// カメラの自己位置を計算するモデル
    /// </summary>
    public class CameraPoseCalculatorModel
    {
        /// <summary>
        /// ターゲットオブジェクトを円の中心とした時のPhi
        /// </summary>
        private float currentPhiAngle = 0;
        
        /// <summary>
        /// ターゲットオブジェクトを円の中心とした時のTheta
        /// </summary>
        private float currentThetaAngle = 90;
        
        /// <summary>
        /// 円の半径
        /// </summary>
        private float radius;
        
        public void InitializePose(Camera camera, Transform target)
        {
            currentPhiAngle= CalculateInitialAngle(camera);
            radius = CalculateRadius(camera, target);
        }
        
        /// <summary>
        /// カメラの初期位置をCameraView.Camera.Transformに合わせる用の角度
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        private float CalculateInitialAngle(Camera camera)
        {
            var from = camera.transform.position;
            var to = Vector3.right;
            
            var projection = Vector3.ProjectOnPlane(from, Vector3.up);
            var result = Vector3.SignedAngle(projection, to, Vector3.up);

            return result;
        }

        private float CalculateRadius(Camera camera, Transform target)
        {
            var x = Mathf.Abs(camera.transform.position.x - target.position.x);
            var z = Mathf.Abs(camera.transform.position.z - target.position.z);
            
            return Mathf.Sqrt(Mathf.Pow(x,2) + Mathf.Pow(z,2));
        }
        
        /// <summary>
        /// カメラの位置を計算するメソッド
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="camera"></param>
        /// <param name="target"></param>
        /// <param name="moveLeftRightSpeed"></param>
        /// <param name="moveUpDownSpeed"></param>
        /// <param name="zoomSpeed"></param>
        /// <returns></returns>
        public Vector3 CalculateCameraPose(
            KeyCode keyCode, 
            Camera camera, 
            Transform target,
            float moveLeftRightSpeed,
            float moveUpDownSpeed)
        {
            switch (keyCode)
            {
                case KeyCode.A:
                    return CalculateCameraPoseAlongCircle(camera, target, -moveLeftRightSpeed, 0);
                case KeyCode.S:
                    return CalculateCameraPoseAlongCircle(camera, target, 0, moveUpDownSpeed);
                case KeyCode.D:
                    return CalculateCameraPoseAlongCircle(camera, target, moveLeftRightSpeed, 0);
                case KeyCode.W:
                    return CalculateCameraPoseAlongCircle(camera, target, 0, -moveUpDownSpeed);
                default:
                    return camera.transform.position;
            }
        }

        public Vector3 CalculateCameraPose(
            Camera camera, 
            Transform target,
            float zoomSpeed)
        {
            return CalculateCameraPoseAlongCircle(camera, target, zoomSpeed);
        }
        
        /// <summary>
        /// 半径radiusの円に沿って移動するときのカメラ位置を計算するメソッド
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="target"></param>
        /// <param name="moveLeftRightSpeed"></param>
        /// <param name="moveUpDownSpeed"></param>
        /// <returns></returns>
        private Vector3 CalculateCameraPoseAlongCircle(
            Camera camera,
            Transform target, 
            float moveLeftRightSpeed,
            float moveUpDownSpeed)
        {
            currentPhiAngle+= moveLeftRightSpeed;
            currentThetaAngle += moveUpDownSpeed;
                        
            if (currentPhiAngle > 360.0f)
            {
                currentPhiAngle -= 360.0f;
            }
            else if (currentPhiAngle < -360.0f)
            {
                currentPhiAngle += 360.0f;
            }

            // Debug.Log($"currentPhiAngle: {currentPhiAngle}, currentThetaAngle: {currentThetaAngle}");
            
            if (currentThetaAngle < 1f)
            {
                currentThetaAngle = 1f;
            }
            else if (currentThetaAngle > 179f)
            {
                currentThetaAngle = 179.0f;
            }
            
            // 角度をラジアンに変換
            var phiRadians = currentPhiAngle* Mathf.Deg2Rad;
            var thetaRadians = currentThetaAngle * Mathf.Deg2Rad;
        
            // 円周上の位置を計算
            var x = target.position.x + radius * Mathf.Sin(thetaRadians) * Mathf.Cos(phiRadians);
            var y = target.position.y + radius * Mathf.Cos(thetaRadians);
            var z = target.position.z + radius * Mathf.Sin(thetaRadians) * Mathf.Sin(phiRadians);
        
            // カメラの位置を設定
            var result = new Vector3(x, y, z);

            return result;
        }
        
        /// <summary>
        /// 半径radiusの円に沿って移動するときのカメラ位置を計算するメソッド
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="target"></param>
        /// <param name="zoomSpeed"></param>
        /// <returns></returns>
        private Vector3 CalculateCameraPoseAlongCircle(
            Camera camera,
            Transform target,
            float zoomSpeed)
        {
            // 角度をラジアンに変換
            var phiRadians = currentPhiAngle* Mathf.Deg2Rad;
            var thetaRadians = currentThetaAngle * Mathf.Deg2Rad;
            
            // ズーム操作
            if ((zoomSpeed > 0 && radius < 10) || (zoomSpeed < 0 && radius > 1))
                radius += zoomSpeed;
            
            // 円周上の位置を計算
            var x = target.position.x + radius * Mathf.Sin(thetaRadians) * Mathf.Cos(phiRadians);
            var y = target.position.y + radius * Mathf.Cos(thetaRadians);
            var z = target.position.z + radius * Mathf.Sin(thetaRadians) * Mathf.Sin(phiRadians);
        
            // カメラの位置を設定
            var result = new Vector3(x, y, z);

            return result;
        }
    }
}