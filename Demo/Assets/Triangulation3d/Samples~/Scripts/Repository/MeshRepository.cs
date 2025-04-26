using System;
using System.Collections.Generic;
using Triangulation3d.Runtime;
using System.Linq;
using UnityEngine;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// meshの情報を管理するリポジトリ
    /// </summary>
    public class MeshRepository
    {
        private readonly Dictionary<string, List<MeshView>> cachedMeshViews = new();
        
        public void SetMeshViews(
            string objectName,
            List<MeshView> meshViews)
        {
            cachedMeshViews[objectName] = meshViews;
        }

        public void Clear()
        {
            cachedMeshViews.Clear();
        }
    }
    
}