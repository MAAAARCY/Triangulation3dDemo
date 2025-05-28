using R3;
using UnityEngine;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// カメラの自己位置を管理するモデル
    /// </summary>
    public class CameraPoseModel
    {
        public readonly ReactiveProperty<float> MoveLeftRightSpeedProperty = new(3.0f);
        
        public readonly ReactiveProperty<float> MoveUpDownSpeedProperty = new(3.0f);
        
        public readonly ReactiveProperty<float> ZoomSpeedProperty = new(3.0f);
        
        private readonly CameraPoseCalculatorModel poseCalculatorModel;

        public CameraPoseModel(CameraPoseCalculatorModel poseCalculatorModel)
        {
            this.poseCalculatorModel = poseCalculatorModel;
        }
        
        private void Dispose()
        {
            MoveLeftRightSpeedProperty.Dispose();
            MoveUpDownSpeedProperty.Dispose();
            ZoomSpeedProperty.Dispose();
        }

        public void InitializePose(Camera camera, Transform target)
        {
            poseCalculatorModel.InitializePose(camera, target);
        }
        public Vector3 GetCameraPose(KeyCode keyCode, Camera camera, Transform target)
        {
            var result = poseCalculatorModel.CalculateCameraPose(
                keyCode: keyCode, 
                camera: camera, 
                target: target,
                moveLeftRightSpeed: MoveLeftRightSpeedProperty.Value,
                moveUpDownSpeed: MoveUpDownSpeedProperty.Value);
            
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