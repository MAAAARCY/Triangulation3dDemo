using System.Collections.Generic;

namespace Triangulation3d.Runtime
{
    /// <summary>
    /// BlenderからJsonファイルに保存したデータをデシリアライズする用のクラス
    /// </summary>
    public class BlenderSurfaces
    {
        public List<Surface> Surfaces { get; set; }
    }
}