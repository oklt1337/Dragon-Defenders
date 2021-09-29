using System;
using _Project.GamePlay.Player.CommanderModel.Library;
using _Project.GamePlay.Player.PlayerModel.Scripts;
using _Project.GamePlay.Spawning.Scripts;
using _Project.Scripts.Gameplay.Enemies;
using _Project.Scripts.Gameplay.Unit;
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
        [SerializeField] private GameObject hq;

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
        public GameObject Hq => hq;

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
        }

        private void Start()
        {
            CurrentGameState = GameState.Wave;
        }

        #endregion

        #region Private Methods

        

        #endregion

        #region Protected Methods

        

        #endregion

        #region Public Methods

        

        #endregion
    }
}
