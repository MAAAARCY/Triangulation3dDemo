using R3;
using System.Text.RegularExpressions;
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

        public void InitializePose(Camera camera, Transform target)
        {
            poseModel.InitializePose(camera, target);
        }

        public Vector3 GetCameraPose(KeyCode keyCode, Camera camera, Transform target)
        {
            var pose = poseModel.GetCameraPose(keyCode, camera, target);
            return pose;
        }

        public Vector3 GetCameraPose(float zoomSpeed, Camera camera, Transform target)
        {
            var pose = poseModel.GetCameraPose(zoomSpeed, camera, target);
            return pose;
        }
    }

}