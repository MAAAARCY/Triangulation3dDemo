using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;

namespace Triangulation3d.Samples
{
    public class CameraSensitivityModel
    {
        private readonly CompositeDisposable disposable = new();
        //private readonly Func<CancellationToken, UniTask> onClickAsync;

        public CameraSensitivityModel()
        {
            
        }
        // public CameraSensitivityModel(Func<CancellationToken, UniTask> onClickAsync)
        // {
        //     this.onClickAsync = onClickAsync;
        // }
    }
}