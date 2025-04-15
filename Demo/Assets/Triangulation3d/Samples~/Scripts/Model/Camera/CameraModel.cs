using R3;
using UnityEngine;

namespace Triangulation3d.Samples
{
    public class CameraModel
    {
        private readonly float moveSpeed = 0.1f;
        private readonly float rotationSpeed = 2f;
        private readonly float smoothTime = 0.1f;

        public CameraModel()
        {
            
        }

        public Vector3 GetCameraMove(KeyCode keyCode)
        {
            var move = CalculateCameraMove(keyCode);
            return move;
        }

        private Vector3 CalculateCameraMove(KeyCode keyCode)
        {
            var result = Vector3.zero;
            switch (keyCode)
            {
                case KeyCode.A:
                    result += Vector3.left * moveSpeed;
                    break;
                case KeyCode.S:
                    result += Vector3.back * moveSpeed;
                    break;
                case KeyCode.D:
                    result += Vector3.right * moveSpeed;
                    break;
                case KeyCode.W:
                    result += Vector3.forward * moveSpeed;
                    break;
                default:
                    break;
            }

            return result;
        }
    }

}