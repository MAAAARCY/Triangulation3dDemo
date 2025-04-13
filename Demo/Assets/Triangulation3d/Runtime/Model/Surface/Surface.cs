using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Triangulation3d.Runtime
{
    /// <summary>
    /// Surface
    /// </summary>
    public class Surface
    {
        /// <summary>
        /// 各面の頂点座標
        /// </summary>
        public List<List<float>> Coordinates { get; set; }
    }
}