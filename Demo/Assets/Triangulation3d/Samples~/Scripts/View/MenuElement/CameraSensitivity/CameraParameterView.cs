using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// カメラのパラメータ調整バー
    /// </summary>
    public class CameraParameterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Slider slider;
        
        public TextMeshProUGUI Title => title;
        public Slider Slider => slider;
    }
}