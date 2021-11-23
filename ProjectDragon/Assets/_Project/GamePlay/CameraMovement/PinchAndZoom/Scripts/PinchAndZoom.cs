using System;
using System.Collections.Generic;
using GamePlay.GameManager.Scripts;
using UnityEngine;

namespace GamePlay.CameraMovement.PinchAndZoom.Scripts
{
    /// <summary>
    /// Author: Christopher Zelch
    /// </summary>
    public class PinchAndZoom : MonoBehaviour
    {
        [SerializeField] private float mouseZoomSpeed = 15.0f;
        [SerializeField] private float touchZoomSpeed = 0.1f;

        [Header("Build")] 
        [SerializeField] private Camera buildCam;
        [SerializeField] private float buildZoomMinBound = 10f;
        [SerializeField] private float buildZoomMaxBound = 32f;
        
        [Header("Bound")]
        [SerializeField] private Vector2 maxTopLeft;
        [SerializeField] private Vector2 minTopLeft;
        [SerializeField] private Vector2 maxBottomRight;
        [SerializeField] private Vector2 minBottomRight;

        [Header("Commander")] 
        [SerializeField] private Camera commanderCam;
        [SerializeField] private float commanderZoomMinBound = 5f;
        [SerializeField] private float commanderZoomMaxBound = 12f;
        
        private Camera cam;
        private Vector3 resetCamPos;
        private float zoomMinBound;
        private float zoomMaxBound;

        private void Awake()
        {
            cam = Camera.current;
            resetCamPos = buildCam.transform.position;
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += SelectCamera;
            GameManager.Scripts.GameManager.Instance.PlayerModel.InputHandler.OnMultiTouch += PinchDetection;
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += ResetBuildCam;
        }

        private void Update()
        {
            
            // This is for Beta Testing should be removed in Build
            if (Input.touchSupported)
                return;
            
            var scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll == 0)
                return;
            
            switch (GameManager.Scripts.GameManager.Instance.CurrentGameState)
            {
                case GameState.Wave:
                    Zoom(-scroll, mouseZoomSpeed);
                    break;
                case GameState.Build:
                {
                    var ray = cam.ScreenPointToRay(Input.mousePosition);
                    var desiredPosition = Physics.Raycast(ray , out var hit) ? hit.point : transform.position;
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

        private void ResetBuildCam(GameState state)
        {
            if (state != GameState.Build) 
                return;
            buildCam.transform.position = resetCamPos;
            buildCam.orthographicSize = buildZoomMaxBound;
        }

        /// <summary>
        /// Calculate old finger distance and new and compares them.
        /// And will Zoom in or out.
        /// </summary>
        /// <param name="touches">List of Touches</param>
        private void PinchDetection(List<Touch> touches)
        {
            if (touches.Count != 2)
                return;
            
            // get current touch positions
            var tZero = touches[0];
            var tOne = touches[1];

            // get touch position from the previous frame
            var tZeroPrevious = tZero.position - tZero.deltaPosition;
            var tOnePrevious = tOne.position - tOne.deltaPosition;

            var oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
            var currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

            // get offset value
            var deltaDistance = oldTouchDistance - currentTouchDistance;
            
            switch (GameManager.Scripts.GameManager.Instance.CurrentGameState)
            {
                case GameState.Wave:
                    Zoom(deltaDistance, touchZoomSpeed);
                    break;
                case GameState.Build:
                {
                    var screenPosition = 0.5f * (tZero.position + tOne.position);
                    var ray = cam.ScreenPointToRay(screenPosition);
                    var desiredPosition = Physics.Raycast(ray , out var hit) ? hit.point : transform.position;
                    
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
            var orthographicSize = cam.orthographicSize;
            orthographicSize += deltaMagnitudeDiff * speed;
            // set min and max value of Clamp function upon your requirement
            cam.orthographicSize = Mathf.Clamp(orthographicSize, zoomMinBound, zoomMaxBound);
        }

        /// <summary>
        /// Camera Zoom in or out towards touches.
        /// </summary>
        /// <param name="deltaMagnitudeDiff">float</param>
        /// <param name="speed">float</param>
        /// <param name="desiredPosition">Vector3</param>
        private void Zoom(float deltaMagnitudeDiff, float speed, Vector3 desiredPosition)
        {
            var orthographicSize = cam.orthographicSize;
            orthographicSize += deltaMagnitudeDiff * speed;
            // set min and max value of Clamp function upon your requirement
            cam.orthographicSize = Mathf.Clamp(orthographicSize, zoomMinBound, zoomMaxBound);
            
            var camPosition = cam.transform.position;
            desiredPosition = new Vector3(desiredPosition.x, camPosition.y, desiredPosition.z);
            var distance = Vector3.Distance(desiredPosition , camPosition);
            var direction = Vector3.Normalize(desiredPosition - camPosition) * (distance / speed);
            camPosition += direction;

            var percentage = (cam.orthographicSize - zoomMinBound) / (zoomMaxBound - zoomMinBound);

            var topLeftX = minTopLeft.x + (percentage * (maxTopLeft.x - minTopLeft.x));
            var topLeftZ = minTopLeft.y + (percentage * (maxTopLeft.y - minTopLeft.y));
            var bottomRightX = minBottomRight.x + (percentage * (maxBottomRight.x - minBottomRight.x));
            var bottomRightZ = minBottomRight.y + (percentage * (maxBottomRight.y - minBottomRight.y));
            
            camPosition.x = Mathf.Clamp(camPosition.x, topLeftX, bottomRightX);
            camPosition.z = Mathf.Clamp(camPosition.z, bottomRightZ, topLeftZ);

            cam.transform.position = camPosition;
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
                    cam = buildCam;
                    zoomMinBound = buildZoomMinBound;
                    zoomMaxBound = buildZoomMaxBound;
                    break;
                case GameState.Wave:
                    cam = commanderCam;
                    zoomMinBound = commanderZoomMinBound;
                    zoomMaxBound = commanderZoomMaxBound;
                    break;
                case GameState.Prepare:
                    break;
                case GameState.End:
                    break;
                default:
                    cam = Camera.current;
                    zoomMinBound = 0;
                    zoomMaxBound = 0;
                    break;
            }
        }
    }
}
