using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        /*
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private WaveManager _waveManager;
        [SerializeField] private UnitManager _unitManager;
        [SerializeField] private Player _player;
        [SerializedField] private GameObject _hq;
        */

        /*
        public EnemySpawner EnemySpawner => _enemySpawner;
        public WaveManager WaveManager => _waveManager;
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
