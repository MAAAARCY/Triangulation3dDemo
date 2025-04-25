using Cysharp.Threading.Tasks;
using iShape.Geometry.Polygon;
using System.Threading;
using UnityEngine;
using ObservableCollections;
using System.Collections.Generic;

namespace Triangulation3d.Samples
{
    public class SelectObjectModel
    {
        // View構築用にpublicにしておく
        public IObservableCollection<SelectableObjectModel> ObservableCollection => hashSet;

        // ObservableなHashSet
        // HashSetは要素の重複を許さないコレクション
        private readonly ObservableHashSet<SelectableObjectModel> hashSet = new();

        private readonly int maxCapacity = 6;

        public async UniTask OnSelectableObjectChangedAsync(CancellationToken cancellationToken)
        {
            Debug.Log("SelectObjectModel.OnSelectableObjectChangedAsync");
            await UniTask.DelayFrame(1, cancellationToken:cancellationToken);
        }

        public bool TryAddSelectableObject(string objectName)
        {
            var selectableObjectModel = new SelectableObjectModel(objectName);
            
            // 要素を超える場合は追加しない
            if (hashSet.Count >= maxCapacity) return false;
            return hashSet.Add(selectableObjectModel);
        }
        
        public void InitializeSelectableObject()
        {
            // 後で消す
            var objectNames = new List<string>()
            {
                "Cube",
                "Suzanne"
            };

            foreach (var name in objectNames)
            {
                var selectableObjectModel = new SelectableObjectModel(name);
                hashSet.Add(selectableObjectModel);
            }
            
            // 要素を超える場合は追加しない
            // if (hashSet.Count >= maxCapacity) return false;
            // return hashSet.Add(selectableObjectModel);
        }
    }
}