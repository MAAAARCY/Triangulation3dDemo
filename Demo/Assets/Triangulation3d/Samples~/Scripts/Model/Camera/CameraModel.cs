using R3;
using UnityEngine;

namespace Triangulation3d.Samples
{
    public class CameraModel
    {
        private readonly CameraPoseModel poseModel;
        
        public CameraModel(
            CameraPoseModel poseModel)
        {
            this.poseModel = poseModel;
        }

        public void InitializePose(Camera camera)
        {
            poseModel.InitializePose(camera);
        }

        public Vector3 GetCameraPose(KeyCode keyCode, Camera camera)
        {
            var pose = poseModel.GetCameraPose(keyCode, camera);
            return pose;
        }
    }

}