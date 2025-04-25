using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// 表示するオブジェクトを選択するUIのView
    /// </summary>
    public class SelectObjectView : BaseMenuContentView
    {
        [SerializeField] private GameObject rootObject;
        [SerializeField] private SelectableObjectView selectableObjectView;
        
        private readonly List<SelectableObjectView> selectableObjectViews = new();
        public SelectableObjectView SelectableObjectView => selectableObjectView;
        
        public GameObject RootObject => rootObject;
        
        //public List<SelectableObjectView> SelectableObjectViews => selectableObjectViews;

    }
}