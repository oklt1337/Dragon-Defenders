using System;
using _Project.GamePlay.Spawning.EnemySpawner.Scripts;
using _Project.GamePlay.Spawning.WaveGenerator.Scripts;
using _Project.GamePlay.Spawning.WaveManager.Scripts;
using _Project.UI.In_Game.Commander.Scripts;
using Dreamteck.Splines;
using GamePlay.CameraMovement.TrackingShot;
using GamePlay.CommanderWaypoint.Scripts;
using GamePlay.HQManager.Scripts;
using GamePlay.Player.PlayerModel.Scripts;
using UI.In_Game.Base_UI.Scripts;
using UI.In_Game.Building.Scripts;
using Units.Unitmanager;
using UnityEngine;

namespace GamePlay.GameManager.Scripts
{
    public enum GameState
    {
        Prepare,
        Build,
        Wave,
        End
    }

    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        #region SerializedFields

        [Header("Player")] [SerializeField] private PlayerModel player;
        [SerializeField] private Transform playerSpawn;

        [Header("Commander")] [SerializeField] private CommanderMoveIndicator commanderMoveIndicator;

        [Header("Managers")] [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private WaveGenerator waveGenerator;
        [SerializeField] private UnitManager unitManager;
        [SerializeField] private HqManager hqManager;

        [Header("Camera")] [SerializeField] private TrackingShotBuildToWave cameraTrackingShotBuildToWave;

        [Header("HUDs")] [SerializeField] private HUD hud;
        [SerializeField] private CommanderHUD commanderHUD;
        [SerializeField] private BuildHUD buildHUD;

        [Header("Enemy")] 
        [SerializeField] private SplineComputer splineComputer;

        #endregion

        #region Private Fields

        private GameState _currentGameState;

        #endregion

        #region Public Properties

        public Transform PlayerSpawn => playerSpawn;

        public GameState CurrentGameState
        {
            get => _currentGameState;
            private set
            {
                _currentGameState = value;
                OnGameStateChanged?.Invoke(CurrentGameState);
            }
        }

        public EnemySpawner EnemySpawner => enemySpawner;
        public WaveManager WaveManager => waveManager;
        public WaveGenerator WaveGenerator => waveGenerator;
        public PlayerModel PlayerModel => player;
        public UnitManager UnitManager => unitManager;
        public HqManager Hq => hqManager;
        public CommanderMoveIndicator CommanderMoveIndicator => commanderMoveIndicator;
        public TrackingShotBuildToWave CameraTrackingShotBuildToWave => cameraTrackingShotBuildToWave;
        public HUD HUD => hud;
        public CommanderHUD CommanderHUD => commanderHUD;
        public BuildHUD BuildHUD => buildHUD;
        public SplineComputer SplineComputer => splineComputer;

        #endregion

        #region Events

        public event Action<GameState> OnGameStateChanged;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            BuildHUD.OnWaveStart += ChangeState;
            enemySpawner.OnWaveSuccess += ChangeState;
            CameraTrackingShotBuildToWave.OnTrackingShotEnd += ChangeState;
        }

        private void Start()
        {
            CurrentGameState = GameState.Build;
        }

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Changes the game state.
        /// </summary>
        /// <param name="state">GameState</param>
        private void ChangeState(GameState state)
        {
            CurrentGameState = state;
        }

        /// <summary>
        /// Ends the game and shows Final screen.
        /// </summary>
        private void EndMatch()
        {
            ChangeState(GameState.End);
            //TODO: Show score and so on.
        }

        #endregion
    }
}
