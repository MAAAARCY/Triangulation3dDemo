using R3;
using UnityEngine;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// カメラの自己位置を管理するモデル
    /// </summary>
    public class CameraPoseModel
    {
        public readonly ReactiveProperty<float> RotationSpeedProperty = new(3.0f);
        
        public readonly ReactiveProperty<float> MoveSpeedProperty = new(3.0f);
        
        public readonly ReactiveProperty<float> ZoomSpeedProperty = new(3.0f);
        
        /// <summary>
        /// カメラの初期位置
        /// </summary>
        private Vector3 initialPosition;
        
        /// <summary>
        /// カメラの回転 or 移動速度
        /// TODO:カメラのパラメータを管理するクラスに分割
        /// </summary>
        // private readonly float moveSpeed = 0.1f;
        // private readonly float rotateSpeed = 3.0f;
        // private readonly float zoomSpeed = 5.0f;
        
        private readonly CameraPoseCalculatorModel poseCalculatorModel;

        public CameraPoseModel(CameraPoseCalculatorModel poseCalculatorModel)
        {
            this.poseCalculatorModel = poseCalculatorModel;
        }
        
        private void Dispose()
        {
            RotationSpeedProperty.Dispose();
            MoveSpeedProperty.Dispose();
            ZoomSpeedProperty.Dispose();
        }

        public void InitializePose(Camera camera, Transform target)
        {
            initialPosition = camera.transform.position;
            
            poseCalculatorModel.InitializePose(camera, target);
        }
        public Vector3 GetCameraPose(KeyCode keyCode, Camera camera, Transform target)
        {
            var result = poseCalculatorModel.CalculateCameraPose(
                keyCode: keyCode, 
                camera: camera, 
                target: target,
                rotationSpeed: RotationSpeedProperty.Value,
                moveSpeed: MoveSpeedProperty.Value);
            
            return result;
        }

        public Vector3 GetCameraPose(float scrollSpeed, Camera camera, Transform target)
        {
            var result = poseCalculatorModel.CalculateCameraPose(
                camera: camera,
                target: target,
                zoomSpeed: scrollSpeed * ZoomSpeedProperty.Value);
            
            return result;
        }
    }
}