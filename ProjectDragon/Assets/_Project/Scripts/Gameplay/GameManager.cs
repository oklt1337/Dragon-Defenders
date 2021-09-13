using _Project.Scripts.Gameplay.Enemies;
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
        /*
        [SerializeField] private UnitManager _unitManager;
        [SerializeField] private Player _player;
        [SerializedField] private GameObject _hq;
        */

        
        public EnemySpawner EnemySpawner => enemySpawner;
        public WaveManager WaveManager => waveManager;
        /*
        public UnitManager UnitManager => _unitManager;
        public Player Player => _player;
        public GameObject HQ => _hq;
        */

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
