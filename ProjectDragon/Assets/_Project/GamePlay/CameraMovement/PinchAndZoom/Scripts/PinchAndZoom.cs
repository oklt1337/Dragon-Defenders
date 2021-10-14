using System.Collections.Generic;
using _Project.GamePlay.GameManager.Scripts;
using UnityEngine;

namespace _Project.GamePlay.CameraMovement.PinchAndZoom.Scripts
{
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
            if (Input.touchSupported)
                return;
            
            //Vector3 mousePos = MouseCursor.
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll == 0)
                return;

            Zoom(-scroll, mouseZoomSpeed);
        }

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
            Zoom(deltaDistance, touchZoomSpeed);
        }

        private void Zoom(float deltaMagnitudeDiff, float speed)
        {
            float orthographicSize = _cam.orthographicSize;
            orthographicSize += deltaMagnitudeDiff * speed;
            // set min and max value of Clamp function upon your requirement
            _cam.orthographicSize = Mathf.Clamp(orthographicSize, _zoomMinBound, _zoomMaxBound);
        }

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
