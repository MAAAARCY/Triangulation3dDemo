using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using Triangulation3d.Runtime;
using UnityEngine;

namespace Triangulation3d.Samples
{
    public class CombinedMeshRepository : IDisposable
    {
        private readonly Dictionary<string, CombinedMeshView> cachedCombinedMeshes = new();
        
        public IReadOnlyCollection<CombinedMeshView> CachedCombinedMeshes
            => cachedCombinedMeshes.Values;
        
        public readonly ReactiveProperty<string> SelectedObjectNameProperty = new();
        
        public readonly ReactiveProperty<string> AddedObjectNameProperty = new();

        public void SetCombinedMesh(
            string objectName,
            CombinedMeshView combinedMeshView)
        {
            // 重複回避
            if (!cachedCombinedMeshes.TryAdd(objectName, combinedMeshView)) return;
            
            cachedCombinedMeshes[objectName] = combinedMeshView;
            
            AddedObjectNameProperty.OnNext(objectName);
        }

        public void Dispose()
        {
            SelectedObjectNameProperty.Dispose();
            AddedObjectNameProperty.Dispose();
        }

        public bool OnSameObjectSelected(string objectName)
        {
            return SelectedObjectNameProperty.Value == objectName;
        }

        public void OnClearCurrentSelected()
        {
            if (SelectedObjectNameProperty.Value == null) return;
            
            var currentSelected = GetCurrentSelectedObject();
            currentSelected.RootObject.SetActive(false);
        }

        public bool TryChangeSelectableObject(string objectName)
        {
            var meshView = CachedCombinedMeshes
                .FirstOrDefault(combinedMeshView => combinedMeshView.MeshFilter.name == objectName);
            
            if (meshView == null) return false;
            
            meshView.RootObject.SetActive(true);
            SelectedObjectNameProperty.OnNext(objectName);
            
            return true;
        }

        private CombinedMeshView GetCurrentSelectedObject()
        {
            return cachedCombinedMeshes[SelectedObjectNameProperty.Value];
        }
    }
}