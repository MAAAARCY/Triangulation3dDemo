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
        /// テキスト
        /// </summary>
        public readonly ReactiveProperty<float> RotationSpeedProperty = new(3.0f);

        public void Dispose()
        {
            RotationSpeedProperty.Dispose();
        }
    }
}