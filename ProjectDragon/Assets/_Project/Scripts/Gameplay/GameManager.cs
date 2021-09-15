using _Project.Scripts.Gameplay.Enemies;
using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Gameplay.Unit;
using UnityEngine;

namespace _Project.Scripts.Gameplay
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

        
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private PlayerModel player;
        [SerializeField] private UnitManager unitManager;
        [SerializeField] private GameObject hq;

        
        public EnemySpawner EnemySpawner => enemySpawner;
        public WaveManager WaveManager => waveManager;
        public PlayerModel PlayerModel => player;
        public UnitManager UnitManager => unitManager;
        public GameObject Hq => hq;

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
    }
}
