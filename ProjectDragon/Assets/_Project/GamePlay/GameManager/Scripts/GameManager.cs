using System;
using _Project.GamePlay.CommanderWaypoint.Scripts;
using _Project.GamePlay.HQManager.Scripts;
using _Project.GamePlay.Player.Commander.CommanderModel.CLibrary;
using _Project.GamePlay.Player.PlayerModel.Scripts;
using _Project.GamePlay.Spawning.EnemySpawner.Scripts;
using _Project.GamePlay.Spawning.WaveManager.Scripts;
using _Project.Scripts.Gameplay.Unit;
using _Project.UI.In_Game.Scripts;
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
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        #region SerializedFields

        [SerializeField] private CommanderLibrary commanderLibrary;
        
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private PlayerModel player;
        [SerializeField] private UnitManager unitManager;
        [SerializeField] private HqManager hqManager;
        [SerializeField] private CommanderMoveIndicator commanderMoveIndicator;
        [SerializeField] private Camera cameraTrackingShot;
        [SerializeField] private HUD hud;
        [SerializeField] private CommanderHUD commanderHUD;
        [SerializeField] private BuildHUD buildHUD;

        #endregion

        #region Private Fields

        private GameState _currentGameState;

        #endregion

        #region Protected Fields

        

        #endregion

        #region Public Fields

        

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
        }

        private void Start()
        {
            cameraTrackingShot.gameObject.SetActive(false);
            CurrentGameState = GameState.Wave;
        }

        #endregion

        #region Private Methods

        

        #endregion

        #region Protected Methods

        

        #endregion

        #region Public Methods

        private void ChangeState(GameState state)
        {
            CurrentGameState = state;
        }

        #endregion
    }
}
