using R3;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Triangulation3d.Samples
{
    public class CameraModel
    {
        private readonly CameraPoseModel poseModel;
        private readonly CameraRepository cameraRepository;

        public Observable<float> OnRotationSpeedAsObservable() 
            => cameraRepository.RotationSpeedProperty.AsObservable();
        
        public Observable<float> OnMoveSpeedAsObservable()
            => cameraRepository.MoveSpeedProperty.AsObservable();
        
        public Observable<float> OnZoomSpeedAsObservable()
            => cameraRepository.ZoomSpeedProperty.AsObservable();

        public CameraModel(CameraPoseModel poseModel, CameraRepository cameraRepository)
        {
            this.poseModel = poseModel;
            this.cameraRepository = cameraRepository;
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

        public void OnRotationSpeedChanged(float rotationSpeed)
        {
            // Debug.Log(rotationSpeed);
            poseModel.RotationSpeedProperty.Value = rotationSpeed;
        }
        
        public void OnMoveSpeedChanged(float moveSpeed)
        {
            // Debug.Log(moveSpeed);
            poseModel.MoveSpeedProperty.Value = moveSpeed;
        }
        
        public void OnZoomSpeedChanged(float zoomSpeed)
        {
            // Debug.Log(zoomSpeed);
            poseModel.ZoomSpeedProperty.Value = zoomSpeed;
        }
    }

}