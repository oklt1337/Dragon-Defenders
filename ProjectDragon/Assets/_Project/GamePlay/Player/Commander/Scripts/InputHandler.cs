using System;
using UnityEngine;

namespace _Project.GamePlay.Player.Commander.Scripts
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

        public event Action<Vector3> OnTouch;
        
        #endregion

        #region Unity Methods

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            Vector3 screenPos = Input.mousePosition;
            screenPos.z = CommanderCam.nearClipPlane;
            Ray ray = CommanderCam.ScreenPointToRay(screenPos);
            if (!Physics.Raycast(ray, out RaycastHit hitInfo))
                return;
            OnTouch?.Invoke(hitInfo.point);
            
            if (Input.touchCount == 0)
                return;
            OnTouch?.Invoke(GetTouchPosInWorldCoord());
        }

        #endregion

        #region Private Methods

        private Vector3 GetTouchPosInWorldCoord()
        {
            Vector3 screenPosMobile = Input.GetTouch(0).position;
            screenPosMobile.z = CommanderCam.nearClipPlane;
            Ray rayMobile = CommanderCam.ScreenPointToRay(screenPosMobile);
            return Physics.Raycast(rayMobile, out RaycastHit hitInfoMobile) ? hitInfoMobile.point : Vector3.zero;
        }
        
        #endregion

        #region Protected Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
