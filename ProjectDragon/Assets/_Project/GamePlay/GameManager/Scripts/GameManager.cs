using System;
using _Project.GamePlay.CameraMovement.TrackingShot;
using _Project.GamePlay.CommanderWaypoint.Scripts;
using _Project.GamePlay.HQManager.Scripts;
using _Project.GamePlay.Player.Commander.CommanderModel.CLibrary;
using _Project.GamePlay.Player.PlayerModel.Scripts;
using _Project.GamePlay.Spawning.EnemySpawner.Scripts;
using _Project.GamePlay.Spawning.WaveManager.Scripts;
using _Project.UI.In_Game.Base_UI.Scripts;
using _Project.UI.In_Game.Building.Scripts;
using _Project.UI.In_Game.Commander.Scripts;
using _Project.Units.Unitmanager;
using UnityEngine;

namespace _Project.GamePlay.GameManager.Scripts
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

        [Header("Player")]
        [SerializeField] private PlayerModel player;
        
        [Header("Commander")]
        [SerializeField] private CommanderLibrary commanderLibrary;
        [SerializeField] private CommanderMoveIndicator commanderMoveIndicator;
        
        [Header("Managers")]
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private UnitManager unitManager;
        [SerializeField] private HqManager hqManager;
        
        [Header("Camera")]
        [SerializeField] private TrackingShotBuildToWave cameraTrackingShotBuildToWave;
        
        [Header("HUDs")]
        [SerializeField] private HUD hud;
        [SerializeField] private CommanderHUD commanderHUD;
        [SerializeField] private BuildHUD buildHUD;

        #endregion

        #region Private Fields

        private GameState _currentGameState;

        #endregion

        #region Public Properties

        public GameState CurrentGameState
        {
            get => _currentGameState;
            private set
            {
                _currentGameState = value;
                OnGameStateChanged?.Invoke(CurrentGameState);
            }
        }

        public CommanderLibrary CommanderLibrary => commanderLibrary;
        
        public EnemySpawner EnemySpawner => enemySpawner;
        public WaveManager WaveManager => waveManager;
        public PlayerModel PlayerModel => player;
        public UnitManager UnitManager => unitManager;
        public HqManager Hq => hqManager;
        public CommanderMoveIndicator CommanderMoveIndicator => commanderMoveIndicator;
        public TrackingShotBuildToWave CameraTrackingShotBuildToWave => cameraTrackingShotBuildToWave;
        public HUD HUD => hud;
        public CommanderHUD CommanderHUD => commanderHUD;
        public BuildHUD BuildHUD => buildHUD;

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
