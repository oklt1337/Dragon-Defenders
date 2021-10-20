using System;
using _Project.GamePlay.GameManager.Scripts;
using _Project.GamePlay.Player.PlayerModel.Scripts;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.GamePlay.CameraMovement.TrackingShot
{
    public class TrackingShotBuildToWave : MonoBehaviour
    {
        [Header("Essentials")]
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private CinemachineDollyCart dollyCart;
        [SerializeField] private CinemachineSmoothPath path;
        [SerializeField] [Range(0,1)] private float speedModifier;

        [Header("Rotation")] 
        [SerializeField] private Quaternion startRotation;
        [SerializeField] private Quaternion endRotation;

        [Header("Ortho")]
        [SerializeField] private float startOrtho;
        [SerializeField] private float endOrtho;

        private bool _startTrackingShot;
        private bool _isBuildToWave = true;
        private float _speed;
        private float _modSpeed;
        private PlayerModel _playerModel;

        public event Action<GameState> OnTrackingShotEnd;

        private void Start()
        {
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += EnableTrackingShot;
            _playerModel = GameManager.Scripts.GameManager.Instance.PlayerModel;
            _speed = dollyCart.m_Speed;
            _modSpeed = _speed * speedModifier;
        }

        private void Update()
        {
            if (!_startTrackingShot)
                return;

            StartTrackingShot();

            // Check if Cart is at pos 1
            if (!(Math.Abs(dollyCart.m_Position - 1) < 0.001f))
                return;

            EndTrackingShot();
        }

        /// <summary>
        /// Starts The Tracking Shot.
        /// </summary>
        private void StartTrackingShot()
        {
            virtualCamera.gameObject.SetActive(true);
            dollyCart.gameObject.SetActive(true);
            
            LerpRotation();
            LerpOrtho();
        }

        /// <summary>
        /// End Tracking Shot.
        /// </summary>
        private void EndTrackingShot()
        {
            dollyCart.gameObject.SetActive(false);
            _speed += _modSpeed;

            if (!(Math.Abs(virtualCamera.m_Lens.OrthographicSize - endOrtho) < 0.05f)) 
                return;
            
            _startTrackingShot = false;
            _speed = dollyCart.m_Speed;
            virtualCamera.gameObject.SetActive(false);
            OnTrackingShotEnd?.Invoke(_isBuildToWave ? GameState.Wave : GameState.Build);
        }

        /// <summary>
        /// Lerp Rotation
        /// </summary>
        private void LerpRotation()
        {
            Quaternion currentRotation = virtualCamera.transform.rotation;
            Quaternion rotation = Quaternion.Lerp(currentRotation, endRotation, _speed * Time.deltaTime);
            virtualCamera.transform.rotation = rotation;
        }

        /// <summary>
        /// Lerp OrthographicSize
        /// </summary>
        private void LerpOrtho()
        {
            float currentOrtho = virtualCamera.m_Lens.OrthographicSize;
            float orthographicSize = Mathf.Lerp(currentOrtho, endOrtho, _speed * Time.deltaTime);
            virtualCamera.m_Lens.OrthographicSize = orthographicSize;
        }

        /// <summary>
        /// Enable Tracking Shot and set all parameter.
        /// </summary>
        /// <param name="gameState">GameState</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void EnableTrackingShot(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Prepare:
                    ModWayPoints();
                    ModOrtho();
                    ModRotation();

                    dollyCart.m_Position = 0;
                    _startTrackingShot = true;
                    break;
                case GameState.Build:
                    _isBuildToWave = true;
                    break;
                case GameState.Wave:
                    _isBuildToWave = false;
                    break;
                case GameState.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }

        /// <summary>
        /// Modify waypoints depending on isBuildToWave
        /// </summary>
        private void ModWayPoints()
        {
            switch (_isBuildToWave)
            {
                case true:
                    path.m_Waypoints[0].position = _playerModel.BuildCamera.transform.position;
                    path.m_Waypoints[1].position = _playerModel.CommanderCamera.transform.position;
                    break;
                case false:
                    path.m_Waypoints[0].position = _playerModel.CommanderCamera.transform.position;
                    path.m_Waypoints[1].position = _playerModel.BuildCamera.transform.position;
                    break;
            }
        }

        /// <summary>
        /// Modify orthographicSize depending on isBuildToWave
        /// </summary>
        private void ModOrtho()
        {
            switch (_isBuildToWave)
            {
                case true:
                    startOrtho = _playerModel.BuildCamera.orthographicSize;
                    endOrtho = _playerModel.CommanderCamera.orthographicSize;
                    break;
                case false:
                    startOrtho = _playerModel.CommanderCamera.orthographicSize;
                    endOrtho = _playerModel.BuildCamera.orthographicSize;
                    break;
            }
            virtualCamera.m_Lens.OrthographicSize = startOrtho;
        }
        
        /// <summary>
        /// Modify rotation depending on isBuildToWave
        /// </summary>
        private void ModRotation()
        {
            switch (_isBuildToWave)
            {
                case true:
                    startRotation = _playerModel.BuildCamera.transform.rotation;
                    endRotation = _playerModel.CommanderCamera.transform.rotation;
                    break;
                case false:
                    startRotation = _playerModel.CommanderCamera.transform.rotation;
                    endRotation = _playerModel.BuildCamera.transform.rotation;
                    break;
            }
            virtualCamera.transform.rotation = startRotation;
        }
    }
}
