using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

/// <summary>
/// Reactから送られてきたコマンドをconsole.Logとして出力する
/// jslibの設定が必要
/// </summary>
public class ReactCommandLogger : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Callback(string message);

    private void ExecuteCallback(string message)
    {
        Callback(message);
    }

    // private void Start()
    // {
    //     # if UNITY_WEBGL == true && UNITY_EDITOR == false
    //         ExecuteCallback("Hello from Unity");
    //     # endif
    //         // Debug.Log("ReactCommandLogger");
    // }

    public void ChangeCameraSensitivity(float value)
    {
        ExecuteCallback($"ChangeCameraSensitivity: {value}");
    }

    public void UploadJsonFile(string jsonFileName)
    {
        ExecuteCallback($"UploadJsonFile: {jsonFileName}");
    }

    public void ChangeSelectableObject(string objectName)
    {
        ExecuteCallback($"ChangeSelectableObject: {objectName}");
    }
}

