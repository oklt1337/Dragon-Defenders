using System;
using System.Collections.Generic;
using GamePlay.Player.PlayerModel.Scripts;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GamePlay.Player.InputHandler.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        #region Private Fields

        private Vector3 mousePos;
        private PlayerModel.Scripts.PlayerModel playerModel;

        #endregion

        #region Public Properties

        public Camera CommanderCam { get; internal set; }

        #endregion

        #region Events

        public event Action<Ray> OnTouch;
        public event Action<List<Touch>> OnMultiTouch;

        #endregion

        #region Unity Methods
        private void Update()
        {
            if (playerModel.CurrentState == State.Blocked)
                return;
            
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
                screenPos.z = CommanderCam.nearClipPlane;
                var ray = CommanderCam.ScreenPointToRay(screenPos);

                OnTouch?.Invoke(ray);
            }
        }

        #endregion

        #region Private Methods

        private Ray GetTouchPosInWorldCoord()
        {
            Vector3 screenPosMobile = Input.GetTouch(0).position;
            screenPosMobile.z = CommanderCam.nearClipPlane;
            Ray rayMobile = CommanderCam.ScreenPointToRay(screenPosMobile);
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

        public void Initialize(PlayerModel.Scripts.PlayerModel model)
        {
            playerModel = model;
        }
        
        #endregion
    }
}
