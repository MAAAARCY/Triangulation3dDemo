﻿using System;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEditor.ShaderKeywordFilter;

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
        [SerializeField] private RectTransform menuElementRect;
        
        public TextMeshProUGUI Title => title;
        public TextMeshProUGUI Description => description;
        public RectTransform MenuElementRect { get { return menuElementRect; } set { menuElementRect = value; } }

        public BaseMenuContentView Content { get { return content; } set { content = value; } }
        
        public GameObject GetDescriptionObject() => description.gameObject.transform.parent.gameObject;
        
        public GameObject GetContentObject() => content.gameObject;
    }
}