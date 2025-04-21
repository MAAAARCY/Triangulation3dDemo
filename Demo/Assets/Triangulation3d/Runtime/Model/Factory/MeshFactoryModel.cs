using iShape.Triangulation.Runtime;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Triangulation3d.Runtime
{
    public class MeshFactoryModel
    {
        private readonly ShapeMeshCreatorExt shapeMeshCreatorExt;
        private readonly MeshView meshViewTemplate;
        
        public MeshFactoryModel(
            // ShapeMeshCreatorExt shapeMeshCreatorExt,
            MeshView meshViewTemplate)
        {
            this.meshViewTemplate = meshViewTemplate;
            
            shapeMeshCreatorExt = new ShapeMeshCreatorExt();
        }
        
        /// <summary>
        /// メッシュを生成する
        /// </summary>
        public async UniTask<MeshView> CreateMeshView(
            Vector3[] hull,
            Vector3[][] holes,
            CancellationToken cancellationToken)
        {
            var mesh = shapeMeshCreatorExt.CreateMesh(
                hull: hull,
                holes: holes
            );
            
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            
            meshViewTemplate.MeshFilter.mesh = mesh;
            
            var meshView = Object.Instantiate(
                meshViewTemplate,
                hull[0], // 最初の頂点をオブジェクトの原点として設定する
                Quaternion.identity);

            meshView.Id = Random.Range(0f, 10f).ToString("0.00");
    
            return meshView;
        }

        public async UniTask<MeshView> CreateMeshView(
            Surface surface,
            CancellationToken cancellationToken)
        {
            var hullVertices = surface?.GetHullVertices();

            if (hullVertices == null)
            {
                throw new NullReferenceException();
            }
            
            var mesh = shapeMeshCreatorExt.CreateMesh(
                hull: hullVertices,
                holes: null);
            
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            
            meshViewTemplate.MeshFilter.mesh = mesh;
            
            var meshView = Object.Instantiate(
                meshViewTemplate,
                hullVertices[0], // 最初の頂点をオブジェクトの原点として設定する
                Quaternion.identity);
            
            return meshView;
        }
    }
}