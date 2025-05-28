using R3;
using UnityEngine;

namespace Triangulation3d.Samples
{
    /// <summary>
    /// JSONファイルをアップロードする用のUIのView
    /// </summary>
    public class JsonFileUploadView : BaseMenuContentView
    {
        public ReactiveProperty<string> JsonContentProperty { get; } = new();
        
        /// <summary>
        /// JSONファイルアップロードボタンがクリックされたときの処理
        /// </summary>
        /// <param name="jsonContent"></param>
        public void OnJsonFileUploadButtonClicked(string jsonContent)
        {
            if (string.IsNullOrEmpty(jsonContent))
            {
                Debug.LogWarning("アップロードされたJSONコンテンツが空です。");
                return;
            }
            
            JsonContentProperty.OnNext(jsonContent);
        }
        
        private void Dispose()
        {
            JsonContentProperty.Dispose();
        }
    }
}