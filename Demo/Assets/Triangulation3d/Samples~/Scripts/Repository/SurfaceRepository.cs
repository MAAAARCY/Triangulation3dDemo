using Cysharp.Threading.Tasks;
using Samples.Triangulation3d.Scripts.Model.API;
using System.Collections.Generic;
using System.Threading;
using Triangulation3d.Runtime;

namespace Triangulation3d.Samples
{
    public class SurfaceRepository
    {
        private readonly SurfaceApiModel surfaceApiModel;
        
        public SurfaceRepository(
            SurfaceApiModel surfaceApiModel)
        {
            this.surfaceApiModel = surfaceApiModel;
        }

        public async UniTask<List<Surface>> GetSurfacesAsync(
            string jsonFilePath,
            CancellationToken cancellationToken)
        {
            var surfaces = await surfaceApiModel.GetSurfaceAsync(jsonFilePath, cancellationToken);

            return surfaces;
        }
    }
}