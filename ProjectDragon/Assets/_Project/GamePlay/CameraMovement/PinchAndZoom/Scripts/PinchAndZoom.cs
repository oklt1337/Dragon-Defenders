using System;
using System.Collections.Generic;
using _Project.GamePlay.GameManager.Scripts;
using UnityEngine;

namespace _Project.GamePlay.CameraMovement.PinchAndZoom.Scripts
{
    /// <summary>
    /// Author: Christopher Zelch
    /// </summary>
    public class PinchAndZoom : MonoBehaviour
    {
        [SerializeField] private float mouseZoomSpeed = 15.0f;
        [SerializeField] private float touchZoomSpeed = 0.1f;

        [Header("Build")] [SerializeField] private Camera buildCam;
        [SerializeField] private float buildZoomMinBound = 10f;
        [SerializeField] private float buildZoomMaxBound = 32f;

        [Header("Commander")] [SerializeField] private Camera commanderCam;
        [SerializeField] private float commanderZoomMinBound = 5f;
        [SerializeField] private float commanderZoomMaxBound = 12f;

        private Camera _cam;
        private float _zoomMinBound;
        private float _zoomMaxBound;

        private void Awake()
        {
            _cam = Camera.current;
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += SelectCamera;
            GameManager.Scripts.GameManager.Instance.PlayerModel.InputHandler.OnMultiTouch += PinchDetection;
        }

        private void Update()
        {
            
            // This is for Beta Testing should be removed in Build
            if (Input.touchSupported)
                return;
            
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll == 0)
                return;
            
            switch (GameManager.Scripts.GameManager.Instance.CurrentGameState)
            {
                case GameState.Wave:
                    Zoom(-scroll, mouseZoomSpeed);
                    break;
                case GameState.Build:
                {
                    Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
                    Vector3 desiredPosition = Physics.Raycast(ray , out RaycastHit hit) ? hit.point : transform.position;
                    Zoom(-scroll, mouseZoomSpeed, desiredPosition);
                    break;
                }
                case GameState.Prepare:
                    break;
                case GameState.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Calculate old finger distance and new and compares them.
        /// And will Zoom in or out.
        /// </summary>
        /// <param name="touches">List of Touches</param>
        private void PinchDetection(List<Touch> touches)
        {
            // get current touch positions
            Touch tZero = touches[0];
            Touch tOne = touches[1];

            // get touch position from the previous frame
            Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
            Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

            float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
            float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

            // get offset value
            float deltaDistance = oldTouchDistance - currentTouchDistance;
            
            switch (GameManager.Scripts.GameManager.Instance.CurrentGameState)
            {
                case GameState.Wave:
                    Zoom(deltaDistance, touchZoomSpeed);
                    break;
                case GameState.Build:
                {
                    Vector3 desiredPosition = 0.5f * (tZero.position + tOne.position);
                    
                    Zoom(deltaDistance, touchZoomSpeed, desiredPosition);
                    break;
                }
                case GameState.Prepare:
                    break;
                case GameState.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Camera Zoom in or out.
        /// </summary>
        /// <param name="deltaMagnitudeDiff">float</param>
        /// <param name="speed">float</param>
        private void Zoom(float deltaMagnitudeDiff, float speed)
        {
            float orthographicSize = _cam.orthographicSize;
            orthographicSize += deltaMagnitudeDiff * speed;
            // set min and max value of Clamp function upon your requirement
            _cam.orthographicSize = Mathf.Clamp(orthographicSize, _zoomMinBound, _zoomMaxBound);
        }

        /// <summary>
        /// Camera Zoom in or out towards touches.
        /// </summary>
        /// <param name="deltaMagnitudeDiff">float</param>
        /// <param name="speed">float</param>
        /// <param name="desiredPosition">Vector3</param>
        private void Zoom(float deltaMagnitudeDiff, float speed, Vector3 desiredPosition)
        {
            float orthographicSize = _cam.orthographicSize;
            orthographicSize += deltaMagnitudeDiff * speed;
            // set min and max value of Clamp function upon your requirement
            _cam.orthographicSize = Mathf.Clamp(orthographicSize, _zoomMinBound, _zoomMaxBound);
            
            Vector3 camPosition = _cam.transform.position;
            desiredPosition = new Vector3(desiredPosition.x, camPosition.y, desiredPosition.z);
            float distance = Vector3.Distance(desiredPosition , camPosition);
            Vector3 direction = Vector3.Normalize( desiredPosition - camPosition) * (distance / speed);
            
            camPosition += direction;
            _cam.transform.position = camPosition;
        }

        /// <summary>
        /// Set active Camera and Bounds
        /// </summary>
        /// <param name="state">GameState</param>
        private void SelectCamera(GameState state)
        {
            switch (state)
            {
                case GameState.Build:
                    _cam = buildCam;
                    _zoomMinBound = buildZoomMinBound;
                    _zoomMaxBound = buildZoomMaxBound;
                    break;
                case GameState.Wave:
                    _cam = commanderCam;
                    _zoomMinBound = commanderZoomMinBound;
                    _zoomMaxBound = commanderZoomMaxBound;
                    break;
                case GameState.Prepare:
                    break;
                case GameState.End:
                    break;
                default:
                    _cam = Camera.current;
                    _zoomMinBound = 0;
                    _zoomMaxBound = 0;
                    break;
            }
        }
    }
}
