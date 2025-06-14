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
            model.InitializePose(view.Camera, view.Target.transform);
            view.Camera.transform.LookAt(view.Target.transform.position);
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            
        }

        private void OnSubscribe()
        {
            view.OnKeyBoardInputAsObservable()
                .Subscribe(OnKeyBoardInput)
                .AddTo(view);

            view.OnMouseHoverAsObservable()
                .Subscribe(OnMouseInput)
                .AddTo(view);
            
            view.OnMouseWheelInputAsObservable()
                .Subscribe(OnMouseWheelInput)
                .AddTo(view);

            model.OnRotationSpeedAsObservable()
                .Subscribe(OnRotationSpeed)
                .AddTo(view);
            
            model.OnMoveSpeedAsObservable()
                .Subscribe(OnMoveSpeed)
                .AddTo(view);
            
            model.OnZoomSpeedAsObservable()
                .Subscribe(OnZoomSpeed)
                .AddTo(view);
        }

        private void OnKeyBoardInput(KeyCode keyCode)
        {
            // TODO:KeyCodeを管理するモデルの作成
            view.Camera.transform.position = model.GetCameraPose(keyCode, view.Camera, view.Target.transform);
            view.Camera.transform.LookAt(view.Target.transform.position);
        }

        private void OnMouseInput(Vector2 mousePosition)
        {
            
        }

        private void OnMouseWheelInput(float scrollSpeed)
        {
            view.Camera.transform.position = model.GetCameraPose(scrollSpeed, view.Camera, view.Target.transform);
            view.Camera.transform.LookAt(view.Target.transform.position);
        }

        private void OnRotationSpeed(float speed)
        {
            model.OnRotationSpeedChanged(speed);
        }

        private void OnMoveSpeed(float speed)
        {
            model.OnMoveSpeedChanged(speed);
        }

        private void OnZoomSpeed(float speed)
        {
            model.OnZoomSpeedChanged(speed);
        }
    }

}