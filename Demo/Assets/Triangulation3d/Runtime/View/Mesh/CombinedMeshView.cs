using UnityEditor;
using UnityEngine;

namespace Triangulation3d.Runtime
{
    public class CombinedMeshView : MonoBehaviour
    {
        [SerializeField] private GameObject rootObject;

        [SerializeField] private MeshFilter meshFilter;

        public GameObject RootObject => rootObject;

        public MeshFilter MeshFilter => meshFilter;
    }
}