using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Player.InputHandler.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        #region SerializeFields

        #endregion

        #region Private Fields

        private Vector3 _mousePos;

        #endregion

        #region Protected Fields

        #endregion

        #region Public Fields

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

                Vector3 screenPos = Input.mousePosition;
                screenPos.z = CommanderCam.nearClipPlane;
                Ray ray = CommanderCam.ScreenPointToRay(screenPos);

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

        #region Protected Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
