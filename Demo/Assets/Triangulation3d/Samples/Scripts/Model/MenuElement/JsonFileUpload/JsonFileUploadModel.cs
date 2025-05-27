using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Triangulation3d.Samples
{
    public class JsonFileUploadModel
    {
        private readonly JsonLoaderModel jsonLoaderModel;
        private readonly SurfaceRepository surfaceRepository;

        public JsonFileUploadModel(
            JsonLoaderModel jsonLoaderModel,
            SurfaceRepository surfaceRepository)
        {
            this.jsonLoaderModel = jsonLoaderModel;
            this.surfaceRepository = surfaceRepository;
        }
        
        public async UniTask OnJsonFileUploadAsync(
            
            string jsonContent, 
            CancellationToken cancellationToken)
        {
            // cancellationToken.ThrowIfCancellationRequested();
            Debug.Log("OnJsonFileUploadAsync is called");
            try
            {
                // JSONコンテンツをロードして処理する
                var surfaces = await jsonLoaderModel.LoadJsonAsync(jsonContent, cancellationToken);
                if (surfaces == null || surfaces.Count == 0)
                {
                    Debug.LogWarning("No surfaces found in the JSON content.");
                    return;
                }
                
                surfaceRepository.SetSurfaces(surfaces);
                Debug.Log($"Loaded {surfaces.Count} surfaces from JSON content.");
            }
            catch (Exception e)
            {
                Debug.LogWarning($"JSONファイルのアップロード中にエラーが発生しました: {e.Message}");
                throw;
            }
        }
    }
}