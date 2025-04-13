using iShape.Triangulation.Runtime;
using Cysharp.Threading.Tasks;
using System.Globalization;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Triangulation3d.Runtime
{
    public class MeshFactoryModel
    {
        private readonly ShapeMeshCreatorExt shapeMeshCreatorExt;
        private readonly MeshView meshViewTemplate;
        
        public MeshFactoryModel(
            ShapeMeshCreatorExt shapeMeshCreatorExt,
            MeshView meshViewTemplate)
        {
            this.shapeMeshCreatorExt = shapeMeshCreatorExt;
            this.meshViewTemplate = meshViewTemplate;
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
            var hullVertices = surface.Coordinates.Select(coordinate =>
            {
                var result = new Vector3(coordinate[0], coordinate[1], coordinate[2]);
                
                return result;
            }).ToArray();
            
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