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
        private readonly Dictionary<string, MeshView> cachedMeshViews = new();
        
        public IReadOnlyCollection<MeshView> CachedMeshViews
            => cachedMeshViews.Values;

        public void SetMesh(MeshView meshView)
        {
            // if (meshViews.ContainsKey(meshView.Id))
            // {
            //     throw new InvalidOperationException($"Mesh view with id {meshView.Id} already exists.");
            // }
            
            cachedMeshViews[meshView.Id] = meshView;
        }
        
        public void SetMeshViews(List<MeshView> meshViews)
        {
            foreach (var meshView in meshViews)
            {
                SetMesh(meshView);
            }
        }

        public void Clear()
        {
            cachedMeshViews.Clear();
        }
    }
    
}