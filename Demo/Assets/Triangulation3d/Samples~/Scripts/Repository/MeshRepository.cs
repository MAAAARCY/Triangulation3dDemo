using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Triangulation3d.Samples
{
    ///<summary>
    /// Triangulationに渡す構造体
    ///</summary>
    public struct TestShape3d
    {
        public Vector3[] Hull;
        public Vector3[][] Holes;
    }
    
    /// <summary>
    /// meshの情報を管理するリポジトリ
    /// </summary>
    public class MeshRepository
    {
        private Dictionary<string, MeshView> meshViews = new(); //後々コレクションに変更
        private TestShape3d[] data3d = new TestShape3d[]
        {
            new TestShape3d
            {
                Hull = new Vector3[]
                {
                    //CCW
                    new Vector3(0, 0, 0),
                    new Vector3(0, 0, 30),
                    new Vector3(-30, 0, 30),
                    new Vector3(-30, 0, 0)
                }
            },
            new TestShape3d
            {
                Hull = new Vector3[]
                {
                    //CW
                    new Vector3(0, 0, 0),
                    new Vector3(-30, 0, 0),
                    new Vector3(-30, 30, 0),
                    new Vector3(0, 30, 0)
                }
            },
            new TestShape3d
            {
                Hull = new Vector3[]
                {
                    //CW
                    new Vector3(0, 0, 0),
                    new Vector3(0, 30, 0),
                    new Vector3(0, 30, 30),
                    new Vector3(0, 0, 30)
                }
            },
            new TestShape3d
            {
                Hull = new Vector3[]
                {
                    //CCW
                    new Vector3(-30, 0, 0),
                    new Vector3(-30, 0, 30),
                    new Vector3(-30, 30, 30),
                    new Vector3(-30, 30, 0)
                }
            },
            new TestShape3d
            {
                Hull = new Vector3[]
                {
                    //CCW
                    new Vector3(0, 0, 30),
                    new Vector3(0, 30, 30),
                    new Vector3(-30, 30, 30),
                    new Vector3(-30, 0, 30)
                }
            },
            new TestShape3d
            {
                Hull = new Vector3[]
                {
                    //CW
                    new Vector3(0, 30, 0),
                    new Vector3(-30, 30, 0),
                    new Vector3(-30, 30, 30),
                    new Vector3(0, 30, 30)
                }
            }
        };

        
        public IReadOnlyCollection<MeshView> MeshViews
            => meshViews.Values;
        public TestShape3d[] Data3d
            => data3d;

        public void SetMesh(MeshView meshView)
        {
            if (meshViews.ContainsKey(meshView.Id))
            {
                throw new InvalidOperationException($"Mesh view with id {meshView.Id} already exists.");
            }
            
            meshViews[meshView.Id] = meshView;
        }

        public void Clear()
        {
            meshViews.Clear();
        }
    }
    
}