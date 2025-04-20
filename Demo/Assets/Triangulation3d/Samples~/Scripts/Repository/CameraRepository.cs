using R3;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// Cameraの情報を格納するリポジトリ
    /// </summary>
    public class CameraRepository
    {
        /// <summary>
        /// テキスト
        /// </summary>
        public readonly ReactiveProperty<float> RotationSpeedProperty = new(3.0f);
    }
}