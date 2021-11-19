using System;
using System.Collections.Generic;
using Deck_Cards.Cards.UnitCard.Scripts;
using Deck_Cards.Decks.Scripts;
using Dreamteck.Splines;
using GamePlay.CameraMovement.TrackingShot;
using GamePlay.CommanderWaypoint.Scripts;
using GamePlay.HQManager.Scripts;
using GamePlay.Player.PlayerModel.Scripts;
using GamePlay.Spawning.EnemySpawner.Scripts;
using GamePlay.Spawning.WaveGenerator.Scripts;
using GamePlay.Spawning.WaveManager.Scripts;
using UI.In_Game.Base_UI.Scripts;
using UI.In_Game.Building.Scripts;
using UI.In_Game.Commander.Scripts;
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
        
        [Header("Cameras")] 
        [SerializeField] private Camera buildCamera;
        [SerializeField] private Camera commanderCamera;

        [Header("Player")] 
        [SerializeField] private PlayerModel player;
        [SerializeField] private Transform playerSpawn;

        [Header("Commander")] 
        [SerializeField] private CommanderMoveIndicator commanderMoveIndicator;

        [Header("Managers")] 
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private WaveGenerator waveGenerator;
        [SerializeField] private HqManager hqManager;
        [SerializeField] private UnitManager.Scripts.UnitManager unitManager;

        [Header("Camera")] 
        [SerializeField] private TrackingShotBuildToWave cameraTrackingShotBuildToWave;

        [Header("HUDs")] 
        [SerializeField] private HUD hud;
        [SerializeField] private CommanderHUD commanderHUD;
        [SerializeField] private BuildHUD buildHUD;

        [Header("Enemy")] 
        [SerializeField] private SplineComputer splineComputer;

        #endregion

        #region Private Fields

        private GameState currentGameState;

        #endregion

        #region Public Properties
        
        public Camera BuildCamera => buildCamera;
        public Camera CommanderCamera => commanderCamera;

        public Transform PlayerSpawn => playerSpawn;

        public GameState CurrentGameState
        {
            get => currentGameState;
            private set
            {
                currentGameState = value;
                OnGameStateChanged?.Invoke(CurrentGameState);
            }
        }

        public EnemySpawner EnemySpawner => enemySpawner;
        public WaveManager WaveManager => waveManager;
        public WaveGenerator WaveGenerator => waveGenerator;
        public PlayerModel PlayerModel => player;
        public HqManager Hq => hqManager;
        public UnitManager.Scripts.UnitManager UnitManager => unitManager;
        public CommanderMoveIndicator CommanderMoveIndicator => commanderMoveIndicator;
        public TrackingShotBuildToWave CameraTrackingShotBuildToWave => cameraTrackingShotBuildToWave;
        public HUD HUD => hud;
        public CommanderHUD CommanderHUD => commanderHUD;
        public BuildHUD BuildHUD => buildHUD;
        public SplineComputer SplineComputer => splineComputer;
        public static Deck DefaultDeck { get; set; }

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
            OnGameStateChanged += ChangeCamera;
        }

        private void Start()
        {
            PlayerModel.ModifyMoney(1000);
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
        
        private void ChangeCamera(GameState state)
        {
            switch (state)
            {
                case GameState.Build:
                    buildCamera.gameObject.SetActive(true);
                    commanderCamera.gameObject.SetActive(false);
                    break;
                case GameState.Wave:
                    buildCamera.gameObject.SetActive(false);
                    commanderCamera.gameObject.SetActive(true);
                    break;
                case GameState.Prepare:
                    buildCamera.gameObject.SetActive(false);
                    commanderCamera.gameObject.SetActive(false);
                    Debug.LogError($"GameState: {state}");
                    break;
                case GameState.End:
                    buildCamera.gameObject.SetActive(false);
                    commanderCamera.gameObject.SetActive(true);
                    Debug.LogError($"GameState: {state}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        #endregion
    }
}
