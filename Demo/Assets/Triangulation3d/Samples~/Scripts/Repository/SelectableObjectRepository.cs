using System.Collections.Generic;
using UnityEngine;

namespace Triangulation3d.Samples
{
    public class SelectableObjectRepository
    {
        private readonly Dictionary<int, GameObject> cachedObjects = new();
        
        public IReadOnlyCollection<GameObject> CachedObjects
            => cachedObjects.Values;

        public void SetSelectableObject(GameObject selectableObject)
        {
            cachedObjects[cachedObjects.Values.Count] = selectableObject;
        }
    }
}