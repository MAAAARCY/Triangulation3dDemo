using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Triangulation3d.Runtime
{
    /// <summary>
    /// Surface
    /// </summary>
    public class Surface : ISurfaceModel
    {
        /// <summary>
        /// 各面の頂点座標
        /// </summary>
        public List<List<float>> Coordinates { get; set; }

        public Vector3[] GetHullVertices()
            => Coordinates
                .Select(coordinate => new Vector3(coordinate[0], coordinate[1], coordinate[2]))
                .ToArray();
    }
}