using Cysharp.Threading.Tasks;
using iShape.Geometry.Polygon;
using System.Threading;
using UnityEngine;
using ObservableCollections;
using System.Collections.Generic;
using System.Linq;

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

        private readonly CombinedMeshRepository combinedMeshRepository;

        public SelectObjectModel(
            CombinedMeshRepository combinedMeshRepository)
        {
            this.combinedMeshRepository = combinedMeshRepository;
        }

        public async UniTask OnSelectableObjectChangedAsync(
            string objectName,
            CancellationToken cancellationToken)
        {
            // Debug.Log("SelectObjectModel.OnSelectableObjectChangedAsync");
            await UniTask.DelayFrame(1, cancellationToken:cancellationToken);
            
            // TODO: Validationクラスの作成
            if (!TryChangeSelectableObject(objectName))
            {
                Debug.LogWarning($"{objectName}は既に選択されているか、未登録のオブジェクトです");
            }
        }

        private bool TryChangeSelectableObject(string objectName)
        {
            if (combinedMeshRepository.OnSameObjectSelected(objectName)) return false;
            
            // 現在選択されているオブジェクトを非表示
            combinedMeshRepository.OnClearCurrentSelected();
            
            // 新しく選択されたオブジェクトを表示
            return combinedMeshRepository.TryChangeSelectableObject(objectName);
        }
        
        /// <summary>
        /// SelectableObjectの追加
        /// jsonFileUpload後に使用
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        private bool TryAddSelectableObject(string objectName)
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
        }
    }
}