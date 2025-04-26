using iShape.Triangulation.Runtime;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
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
        private readonly CombinedMeshView combinedMeshViewTemplate;
        
        public MeshFactoryModel(
            // ShapeMeshCreatorExt shapeMeshCreatorExt,
            MeshView meshViewTemplate,
            CombinedMeshView combinedMeshViewTemplate)
        {
            this.meshViewTemplate = meshViewTemplate;
            this.combinedMeshViewTemplate  = combinedMeshViewTemplate;
            
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

            meshViewTemplate.MeshFilter.sharedMesh = mesh;

            var meshView = Object.Instantiate(
                meshViewTemplate,
                hullVertices[0], // 最初の頂点をオブジェクトの原点として設定する
                Quaternion.identity);

            return meshView;
        }

        public async UniTask<CombinedMeshView> CreateCombinedMeshView(
            List<MeshView> meshViews,
            string filterName,
            CancellationToken cancellationToken)
        {
            // var combineInstances = new List<CombineInstance>();

            var combinedMeshView = Object.Instantiate(combinedMeshViewTemplate, Vector3.zero, Quaternion.identity);
            combinedMeshView.MeshFilter.name = filterName;
            
            foreach (var meshView in meshViews)
            {
                meshView.GetGameObject().transform.parent = combinedMeshView.RootObject.transform;
            }
            
            combinedMeshView.RootObject.SetActive(false);
            // var count = 0;
            // foreach (var meshView in meshViews)
            // {
            //     // ローカル→ワールド→結合先のローカル座標に変換するための行列
            //     // var worldToLocalMatrix = combinedMeshViewTemplate.CombinedMeshObject.transform.worldToLocalMatrix;
            //     // var localToWorldMatrix = meshView.GetGameObject().transform.localToWorldMatrix;
            //     // Matrix4x4 transformMatrix = worldToLocalMatrix * localToWorldMatrix;
            //     
            //     CombineInstance combineInstance = new CombineInstance
            //     {
            //         mesh = meshView.MeshFilter.sharedMesh,
            //         // subMeshIndex = count,
            //         transform = meshView.GetGameObject().transform.localToWorldMatrix
            //     };
            //     
            //     combineInstances.Add(combineInstance);
            //     count++;
            // }
            //
            // Mesh combinedMesh = new Mesh();
            // combinedMesh.name = "Sample";
            // combinedMesh.CombineMeshes(combineInstances.ToArray(), false);
            //
            // var combinedMeshView = Object.Instantiate(
            //     combinedMeshViewTemplate,
            //     Vector3.zero,
            //     Quaternion.identity);
            //
            // combineMeshView.MeshFilter.sharedMesh = combinedMesh;
            
            return combinedMeshView;
        }
    }
}