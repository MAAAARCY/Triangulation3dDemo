using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine;

namespace Triangulation3d.Samples
{
    public class CameraSensitivityModel
    {
        private readonly CompositeDisposable disposable = new();
        // }
        
        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            disposable.Dispose();
        }
        
        
        public async UniTask OnClickCameraSensitivityAsync(CancellationToken cancellationToken)
        {
            Debug.Log("CameraSensitivity OnClickCameraSensitivityAsync");
            await UniTask.Yield();
        }
    }
}