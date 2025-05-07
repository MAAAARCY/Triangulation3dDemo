using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Threading;
using Triangulation3d.Runtime;

namespace Triangulation3d.Samples
{
    public class JsonLoaderModel : IDisposable
    {
        private AsyncOperationHandle<TextAsset> handle;
        
        public async UniTask<List<Surface>> ReadAllTextAsync(
            string jsonFilePath,
            CancellationToken cancellationToken)
        {
            var result = new List<Surface>();
            
            try
            {
                handle = Addressables.LoadAssetAsync<TextAsset>(jsonFilePath);
                
                await handle.Task;
            
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    TextAsset jsonAsset = handle.Result;
                    Debug.Log("JSON asset loaded successfully: " + jsonAsset.name);
                
                    var blenderSurfaces = JsonConvert.DeserializeObject<BlenderSurfaces>(jsonAsset.text);
                    result = blenderSurfaces.Surfaces;
                }
                else
                {
                    Debug.LogError("Failed to load JSON asset: " + handle.OperationException);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading JSON asset: " + e.Message);
            }
            
            return result;
        }

        public void Dispose()
        {
            Addressables.Release(handle);
        }
    }
}