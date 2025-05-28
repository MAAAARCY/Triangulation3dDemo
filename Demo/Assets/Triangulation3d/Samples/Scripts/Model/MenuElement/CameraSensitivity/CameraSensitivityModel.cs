using Cysharp.Threading.Tasks;
using R3;
using System.Threading;

namespace Triangulation3d.Samples
{
    public class CameraSensitivityModel
    {
        private readonly CompositeDisposable disposable = new();
        private readonly CameraRepository cameraRepository;

        public CameraSensitivityModel(CameraRepository cameraRepository)
        {
            this.cameraRepository = cameraRepository;
        }
        
        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            disposable.Dispose();
        }
        
        public async UniTask OnRotationSpeedChangedAsync(
            float rotationSpeed,
            CancellationToken cancellationToken)
        {
            cameraRepository.RotationSpeedProperty.OnNext(rotationSpeed);
            await UniTask.Yield();
        }
        
        public async UniTask OnMoveSpeedChangedAsync(
            float moveSpeed,
            CancellationToken cancellationToken)
        {
            cameraRepository.MoveSpeedProperty.OnNext(moveSpeed);
            await UniTask.Yield();
        }
        
        public async UniTask OnZoomSpeedChangedAsync(
            float zoomSpeed,
            CancellationToken cancellationToken)
        {
            cameraRepository.ZoomSpeedProperty.OnNext(zoomSpeed);
            await UniTask.Yield();
        }

        public async UniTask OnClickCameraSensitivityAsync(CancellationToken cancellationToken)
        {
            await UniTask.Yield();
        }
    }
}