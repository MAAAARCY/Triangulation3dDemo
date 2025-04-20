using UnityEngine;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// 表示するオブジェクトを選択するUIのView
    /// </summary>
    public class SelectObjectView : BaseMenuContentView
    {
        [SerializeField] private SelectableObjectView selectableObjectView;
    }
}