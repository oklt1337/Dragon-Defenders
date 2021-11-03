using System;
using Cinemachine;
using GamePlay.GameManager.Scripts;
using GamePlay.Player.PlayerModel.Scripts;
using UnityEngine;

namespace GamePlay.CameraMovement.TrackingShot
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

        private bool startTrackingShot;
        private bool isBuildToWave = true;
        private float speed;
        private float modSpeed;
        private PlayerModel playerModel;

        public event Action<GameState> OnTrackingShotEnd;

        private void Start()
        {
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += EnableTrackingShot;
            playerModel = GameManager.Scripts.GameManager.Instance.PlayerModel;
            speed = dollyCart.m_Speed;
            modSpeed = speed * speedModifier;
        }

        private void Update()
        {
            if (!startTrackingShot)
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
            speed += modSpeed;

            if (!(Math.Abs(virtualCamera.m_Lens.OrthographicSize - endOrtho) < 0.05f)) 
                return;
            
            startTrackingShot = false;
            speed = dollyCart.m_Speed;
            virtualCamera.gameObject.SetActive(false);
            OnTrackingShotEnd?.Invoke(isBuildToWave ? GameState.Wave : GameState.Build);
        }

        /// <summary>
        /// Lerp Rotation
        /// </summary>
        private void LerpRotation()
        {
            var currentRotation = virtualCamera.transform.rotation;
            var rotation = Quaternion.Lerp(currentRotation, endRotation, speed * Time.deltaTime);
            virtualCamera.transform.rotation = rotation;
        }

        /// <summary>
        /// Lerp OrthographicSize
        /// </summary>
        private void LerpOrtho()
        {
            var currentOrtho = virtualCamera.m_Lens.OrthographicSize;
            var orthographicSize = Mathf.Lerp(currentOrtho, endOrtho, speed * Time.deltaTime);
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
                    startTrackingShot = true;
                    break;
                case GameState.Build:
                    isBuildToWave = true;
                    break;
                case GameState.Wave:
                    isBuildToWave = false;
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
            switch (isBuildToWave)
            {
                case true:
                    path.m_Waypoints[0].position = playerModel.BuildCamera.transform.position;
                    path.m_Waypoints[1].position = playerModel.CommanderCamera.transform.position;
                    break;
                case false:
                    path.m_Waypoints[0].position = playerModel.CommanderCamera.transform.position;
                    path.m_Waypoints[1].position = playerModel.BuildCamera.transform.position;
                    break;
            }
        }

        /// <summary>
        /// Modify orthographicSize depending on isBuildToWave
        /// </summary>
        private void ModOrtho()
        {
            switch (isBuildToWave)
            {
                case true:
                    startOrtho = playerModel.BuildCamera.orthographicSize;
                    endOrtho = playerModel.CommanderCamera.orthographicSize;
                    break;
                case false:
                    startOrtho = playerModel.CommanderCamera.orthographicSize;
                    endOrtho = playerModel.BuildCamera.orthographicSize;
                    break;
            }
            virtualCamera.m_Lens.OrthographicSize = startOrtho;
        }
        
        /// <summary>
        /// Modify rotation depending on isBuildToWave
        /// </summary>
        private void ModRotation()
        {
            switch (isBuildToWave)
            {
                case true:
                    startRotation = playerModel.BuildCamera.transform.rotation;
                    endRotation = playerModel.CommanderCamera.transform.rotation;
                    break;
                case false:
                    startRotation = playerModel.CommanderCamera.transform.rotation;
                    endRotation = playerModel.BuildCamera.transform.rotation;
                    break;
            }
            virtualCamera.transform.rotation = startRotation;
        }
    }
}
