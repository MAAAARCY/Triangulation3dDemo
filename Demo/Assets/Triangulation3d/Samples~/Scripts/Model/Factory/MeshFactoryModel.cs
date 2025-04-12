using Cysharp.Threading.Tasks;
using iShape.Triangulation.Runtime;
using System.Threading;
using UnityEngine;

namespace Triangulation3d.Samples
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
    }
}