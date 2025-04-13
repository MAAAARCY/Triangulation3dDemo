using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Triangulation3d.Runtime
{
    /// <summary>
    /// Surfaceのインターフェース
    /// </summary>
    public interface ISurfaceModel
    {
        public List<List<float>> Coordinates { get; set; }
        
        public Vector3[] GetHullVertices();
    }
}