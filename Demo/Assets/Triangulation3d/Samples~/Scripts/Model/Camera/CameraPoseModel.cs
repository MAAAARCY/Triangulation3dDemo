using UnityEngine;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// カメラの自己位置を管理するモデル
    /// </summary>
    public class CameraPoseModel
    {
        /// <summary>
        /// カメラの初期位置
        /// </summary>
        private Vector3 initialPosition;
        
        private readonly CameraPoseCalculatorModel poseCalculatorModel;

        public CameraPoseModel(CameraPoseCalculatorModel poseCalculatorModel)
        {
            this.poseCalculatorModel = poseCalculatorModel;
        }

        public void InitializePose(Camera camera)
        {
            initialPosition = camera.transform.position;
            poseCalculatorModel.InitializePose(camera);
        }
        public Vector3 GetCameraPose(KeyCode keyCode, Camera camera)
        {
            var result = poseCalculatorModel.CalculateCameraPose(keyCode, camera);
            return result;
        }
    }
}