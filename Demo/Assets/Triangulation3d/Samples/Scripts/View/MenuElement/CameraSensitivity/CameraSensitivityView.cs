using TMPro;
using UnityEngine;
using UnityEngine.UI;
using R3;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// カメラの感度を調整するUIのView
    /// </summary>
    public class CameraSensitivityView : BaseMenuContentView
    {
        [SerializeField] private CameraParameterView cameraParameterView;

        [SerializeField] private Slider cameraSensitivitySlider;

        public CameraParameterView CameraParameterView => cameraParameterView;

        public ReactiveProperty<float> RotationSpeedProperty { get; } = new(3.0f);
        public ReactiveProperty<float> MoveSpeedProperty { get; } = new(3.0f);
        public ReactiveProperty<float> ZoomSpeedProperty { get; } = new(3.0f);
        
        private void Dispose()
        {
            RotationSpeedProperty.Dispose();
            MoveSpeedProperty.Dispose();
            ZoomSpeedProperty.Dispose();
        }
        
        public void ChangeRotateSensitivity(float value)
        {
            Debug.Log($"ChangeCameraSensitivity: {value}");
            RotationSpeedProperty.Value = value;
        }
        
        public void ChangeMoveSensitivity(float value)
        {
            Debug.Log($"ChangeMoveSensitivity: {value}");
            MoveSpeedProperty.Value = value;
        }
        
        public void ChangeZoomSensitivity(float value)
        {
            Debug.Log($"ChangeZoomSensitivity: {value}");
            ZoomSpeedProperty.Value = value;
        }
    }
}