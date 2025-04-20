using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine;
using Object = System.Object;

namespace Triangulation3d.Samples
{
    public class MenuElementModel : IDisposable
    {
        private readonly CompositeDisposable disposable = new();
        private readonly Func<CancellationToken, UniTask> onClickAsync;

        /// <summary>
        /// テキスト
        /// </summary>
        public readonly ReactiveProperty<string> TextProperty;
        
        /// <summary>
        /// Descriptionのフラグ
        /// </summary>
        public readonly ReactiveProperty<bool> DescriptionProperty;
        
        /// <summary>
        /// MenuElementType
        /// </summary>
        public readonly ReactiveProperty<MenuElementType> MenuElementTypeProperty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MenuElementModel(
            string text,
            Func<CancellationToken, UniTask> onClickAsync,
            MenuElementType elementType,
            bool descriptionEnable = false)
        {
            TextProperty = new ReactiveProperty<string>(text);
            DescriptionProperty = new ReactiveProperty<bool>(descriptionEnable);
            MenuElementTypeProperty = new ReactiveProperty<MenuElementType>(elementType);
            this.onClickAsync = onClickAsync;
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            disposable.Dispose();
        }
        
        /// <summary>
        /// クリックを通知
        /// </summary>
        public async UniTask ClickAsync(CancellationToken cancellationToken)
        {
            await onClickAsync.Invoke(cancellationToken);
        }

    }
}