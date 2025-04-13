using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// JsonからDeserializeした直後のSurfaceModel
    /// </summary>
    public interface ISurfaceModel
    {
        public string ObjectName { get; set; }
        public List<Vertex> Vertices { get; set; }
        public List<Face> Faces { get; set; }

        public struct Vertex
        {
            public int VertexIndex { get; set; }
            public Vector3[] Coordinates { get; set; }
        }

        public struct Face
        {
            public int FaceIndex { get; set; }
            public List<int> Indices { get; set; }
        }
    }
}