using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Triangulation3d.Samples.UI
{
    /// <summary>
    /// 選択可能なカラーのUIのView
    /// </summary>
    public class ColorParameterView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Button button;

        public Image Icon => icon;
        public Button Button => button;
    }
}