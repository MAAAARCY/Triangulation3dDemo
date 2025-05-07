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
        private readonly Surface surface;

        // private AsyncOperationHandle<string> handle;
        private static readonly HashSet<(string key, AsyncOperationHandle handle)> handles = new ();
        private readonly JsonLoaderModel jsonLoaderModel;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SurfaceApiModel(
            Surface surface,
            JsonLoaderModel jsonLoaderModel)
        {
            this.surface = surface;
            this.jsonLoaderModel = jsonLoaderModel;
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                // await InitAsync();
                // await LoadAssetAsync(_cts.Token);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Addressablesの処理に失敗: {ex}");
            }
        }

        /// <summary>
        /// Addressables全体の初期化
        /// Addressablesを管理するクラスを作成後削除
        /// </summary>
        private async UniTask InitAsync()
        {
            try
            {
                // Addressablesの初期化を待機
                var initHandle = Addressables.InitializeAsync();
                await initHandle.Task;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during Addressables initialization or asset loading: {ex}");
            }
        }
        
        /// <summary>
        /// アセットの読み込み.
        /// </summary>
        /// <param name="assetKey"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async UniTask<T> LoadAssetAsync<T>(string assetKey, CancellationToken cancellationToken)
            where T : UnityEngine.Object
        {
            // ローカルのアセットをロード
            if (handles.All(x => x.key != assetKey))
            {
                handles.Add((assetKey, Addressables.LoadAssetAsync<T>(assetKey)));
            }
            var handleObj = handles.FirstOrDefault(x => x.key == assetKey).handle;
            var typedHandle = handleObj.Convert<T>();

            // アセットのロード完了を待機
            var asset = await typedHandle.ToUniTask(cancellationToken: cancellationToken);
            return asset;
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

        public void Dispose()
        {
            foreach (var handle in handles)
            {
                Addressables.Release(handle);
            }
            
            
        }
    }
}