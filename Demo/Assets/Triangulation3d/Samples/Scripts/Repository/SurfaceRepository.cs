using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Collections.Generic;
using System.Threading;
using Triangulation3d.Runtime;
using UnityEngine;

namespace Triangulation3d.Samples
{
    public class SurfaceRepository : IDisposable
    {
        private readonly SurfaceApiModel surfaceApiModel;

        private readonly Dictionary<string, List<Surface>> cachedSurfaces = new();

        public Dictionary<string, List<Surface>> CachedSurfaces => cachedSurfaces;

        public ReactiveProperty<string> SurfaceNameProperty => new("Sample");
        
        public readonly Subject<string> SurfaceAddedSubject = new();

        public SurfaceRepository(
            SurfaceApiModel surfaceApiModel)
        {
            this.surfaceApiModel = surfaceApiModel;
        }

        public void Dispose()
        {
            cachedSurfaces.Clear();
            SurfaceNameProperty.Dispose();
        }

        public async UniTask<List<Surface>> GetSurfacesAsync(
            string objectName,
            CancellationToken cancellationToken)
        {
            var surfaces = await surfaceApiModel.GetSurfaceAsync(objectName, cancellationToken);

            // SurfaceNameProperty.OnNext(objectName);
            return surfaces;
        }

        public void SetSurfaces(string objectName, List<Surface> surfaces)
        {
            cachedSurfaces[objectName] = surfaces;
            Debug.Log($"SetSurface Name: {objectName}");
            Debug.Log($"Surfaces Count: {surfaces.Count}");
            SurfaceNameProperty.OnNext(objectName);
        }
        
        public void SetSurfaces(List<Surface> surfaces)
        {
            var objectName = $"Object {cachedSurfaces.Count-2}";
            cachedSurfaces[objectName] = surfaces;
            Debug.Log($"SetSurface Name: {objectName}");
            Debug.Log($"Surfaces Count: {surfaces.Count}");
            SurfaceNameProperty.OnNext(objectName);
        }
    }
}