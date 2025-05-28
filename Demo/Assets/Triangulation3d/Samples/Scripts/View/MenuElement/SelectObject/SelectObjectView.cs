using R3;
using System.Collections.Generic;
using UnityEngine;

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
        
        public ReactiveProperty<string> SelectableObjectNameProperty { get; } = new();
        
        public void ChangeSelectableObject(string objectName)
        {
            Debug.Log($"ChangeSelectableObject: {objectName}");
            SelectableObjectNameProperty.Value = objectName;
        }

    }
}