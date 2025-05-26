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

        public ReactiveProperty<float> SliderValueProperty { get; } = new(1.0f);
        
        public void ChangeCameraSensitivity(float value)
        {
            // Debug.Log($"ChangeCameraSensitivity: {value}");
            SliderValueProperty.Value = value;
        }
    }
}