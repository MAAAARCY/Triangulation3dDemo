using Cysharp.Threading.Tasks;
using System.Threading;
using Triangulation3d.Runtime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using VContainer.Unity;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// SurfaceApiModel
    /// </summary>
    public class SurfaceApiModel : IAsyncStartable
    {
        private readonly JsonLoaderModel jsonLoaderModel;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SurfaceApiModel(
            JsonLoaderModel jsonLoaderModel)
        {
            this.jsonLoaderModel = jsonLoaderModel;
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await UniTask.Yield();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Addressablesの処理に失敗: {ex}");
            }
        }
        
        /// <summary>
        /// jsonFileから頂点データを取得
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async UniTask<List<Surface>> GetSurfaceAsync(
            string objectName, 
            CancellationToken cancellationToken)
        {
            var result = await jsonLoaderModel.ReadAllTextAsync($"{objectName}Vertices.json", cancellationToken);
            
            return result;
        }
    }
}