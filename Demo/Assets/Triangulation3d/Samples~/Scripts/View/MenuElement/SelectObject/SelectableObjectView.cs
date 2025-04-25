using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// 選択可能オブジェクトのUIのView
    /// </summary>
    public class SelectableObjectView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI objectName;
        [SerializeField] private Button button;
        public Button Button => button;

        public void Initialize(string name)
        {
            this.objectName.SetText(name);
        }
    }
}