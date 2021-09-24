using System;
using UnityEngine;

namespace _Project.GamePlay.Player.Commander.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private Camera commanderCam;

        #endregion

        #region Private Fields
        
        private Vector3 _mousePos;

        #endregion

        #region Protected Fields

        #endregion

        #region Public Fields

        #endregion

        #region Public Properties

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
            screenPos.z = commanderCam.nearClipPlane;
            Ray ray = commanderCam.ScreenPointToRay(screenPos);
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
            screenPosMobile.z = commanderCam.nearClipPlane;
            Ray rayMobile = commanderCam.ScreenPointToRay(screenPosMobile);
            return Physics.Raycast(rayMobile, out RaycastHit hitInfoMobile) ? hitInfoMobile.point : Vector3.zero;
        }
        
        #endregion

        #region Protected Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
