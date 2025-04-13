using Cysharp.Threading.Tasks;
using System.Threading;
using Triangulation3d.Runtime;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Samples.Triangulation3d.Scripts.Model.API
{
    /// <summary>
    /// SurfaceApiModel
    /// </summary>
    public class SurfaceApiModel
    {
        private readonly Surface surface;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SurfaceApiModel(
            Surface surface)
        {
            this.surface = surface;
        }

        public async UniTask<List<Surface>> GetSurfaceAsync(
            string jsonFilePath, 
            CancellationToken cancellationToken)
        {
            var content = File.ReadAllText(jsonFilePath);
            var blenderSurfaces = JsonConvert.DeserializeObject<BlenderSurfaces>(content);
            
            return blenderSurfaces.Surfaces;
        }
    }
}