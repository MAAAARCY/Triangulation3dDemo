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
        public void OnJsonFileUploadButtonClicked(string jsonContent)
        {
            // JSONファイルアップロードボタンがクリックされたときの処理
            Debug.Log("JSONファイルアップロードボタンがクリックされました。");
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