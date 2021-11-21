using System;
using System.Collections.Generic;
using GamePlay.GameManager.Scripts;
using GamePlay.Player.PlayerModel.Scripts;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GamePlay.Player.InputHandler.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        #region Private Fields

        private Vector3 mousePos;
        private Camera activeCamera;

        #endregion

        #region Public Properties

        public Camera CommanderCam { get; internal set; }
        public Camera BuildCamera { get; internal set; }

        #endregion

        #region Events

        public event Action<Ray> OnTouch;
        public event Action<List<Touch>> OnMultiTouch;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += SwitchActiveCamera;
        }

        private void Update()
        {
            if (Input.touchSupported)
            {
                switch (Input.touchCount)
                {
                    case 1:
                        OnTouch?.Invoke(GetTouchPosInWorldCoord());
                        break;
                    case 2:
                        OnMultiTouch?.Invoke(GetTouches());
                        break;
                }
            }
            else
            {
                if (!Input.GetMouseButtonDown(0))
                    return;

                var screenPos = Input.mousePosition;
                screenPos.z = activeCamera.nearClipPlane;
                var ray = activeCamera.ScreenPointToRay(screenPos);

                OnTouch?.Invoke(ray);
            }
        }

        #endregion

        #region Private Methods

        private void SwitchActiveCamera(GameState state)
        {
            activeCamera = state switch
            {
                GameState.Prepare => BuildCamera,
                GameState.Build => BuildCamera,
                GameState.Wave => CommanderCam,
                GameState.End => BuildCamera,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }

        private Ray GetTouchPosInWorldCoord()
        {
            Vector3 screenPosMobile = Input.GetTouch(0).position;
            screenPosMobile.z = activeCamera.nearClipPlane;
            var rayMobile = activeCamera.ScreenPointToRay(screenPosMobile);
            return rayMobile;
        }

        private List<Touch> GetTouches()
        {
            var touches = new List<Touch>
            {
                Input.GetTouch(0),
                Input.GetTouch(1)
            };

            return touches;
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            activeCamera = BuildCamera;
        }
        
        #endregion
    }
}
