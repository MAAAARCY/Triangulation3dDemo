using R3;
using System;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// Cameraの情報を格納するリポジトリ
    /// </summary>
    public class CameraRepository : IDisposable
    {
        /// <summary>
        /// 回転速度を保持するReactiveProperty
        /// </summary>
        public readonly ReactiveProperty<float> RotationSpeedProperty = new(3.0f);
        
        /// <summary>
        /// 移動速度を保持するReactiveProperty
        /// </summary>
        public readonly ReactiveProperty<float> MoveSpeedProperty = new(3.0f);
        
        /// <summary>
        /// ズーム速度を保持するReactiveProperty
        /// </summary>
        public readonly ReactiveProperty<float> ZoomSpeedProperty = new(3.0f);

        public void Dispose()
        {
            RotationSpeedProperty.Dispose();
            MoveSpeedProperty.Dispose();
            ZoomSpeedProperty.Dispose();
        }
    }
}