﻿using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using R3;
using System.Threading;
using VContainer.Unity;

namespace Triangulation3d.Samples
{
    public class CameraPresenter : IAsyncStartable
    {
        private readonly CameraModel model;
        private readonly CameraView view;

        public CameraPresenter(
            CameraModel model,
            CameraView view)
        {
            this.model = model;
            this.view = view;

            OnSubscribe();
            model.InitializePose(view.Camera);
            view.Camera.transform.LookAt(view.Target.transform.position);
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            
        }

        private void OnSubscribe()
        {
            view.OnKeyBoardInputAsObservable()
                //.Do(code => Debug.Log(code))
                .Subscribe(OnKeyBoardInput)
                .AddTo(view);

            view.OnMouseHoverAsObservable()
                .Subscribe(OnMouseInput)
                .AddTo(view);

        }

        private void OnKeyBoardInput(KeyCode keyCode)
        {
            // TODO:KeyCodeを管理するモデルの作成
            view.Camera.transform.position = model.GetCameraPose(keyCode, view.Camera);
            view.Camera.transform.LookAt(view.Target.transform.position);
        }

        private void OnMouseInput(Vector2 mousePosition)
        {
            
        }
    }

}