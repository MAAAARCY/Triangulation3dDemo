using System;
using UnityEngine;
using TMPro;
using UnityEditor;

namespace Triangulation3d.Samples.UI
{
    /// <summary>
    /// メニュー項目のベースコンポーネント
    /// </summary>
    public class BaseMenuElementView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private BaseMenuContentView content;
        
        public TextMeshProUGUI Title => title;
        public TextMeshProUGUI Description => description;

        public BaseMenuContentView Content { get { return content; } set { content = value; } }
        
        public GameObject GetDescriptionObject() => description.gameObject.transform.parent.gameObject;
        
        public GameObject GetContentObject() => content.gameObject;
    }
}