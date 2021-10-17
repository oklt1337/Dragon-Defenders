using System;
using System.Collections;
using System.Collections.Generic;
using _Project.AI.Enemies.Scripts;
using _Project.GamePlay.GameManager.Scripts;
using Photon.Pun;
using UnityEngine;

namespace _Project.GamePlay.Spawning.EnemySpawner.Scripts
{
    public class EnemySpawner : MonoBehaviourPun
    {
        [Header("Enemy Related Stuff")] [SerializeField]
        private int waveSize;

        [SerializeField] private int killedEnemies;
        [SerializeField] private float enemySpawnDelay;

        private List<GameObject> _enemies;

        [Header("Object Related Stuff")] 
        [SerializeField] private List<Transform> spawnPositions;

        private int _currentSpawnPosition = 0;
        private bool _coroutineIsRunning;

        public event Action<GameState> OnWaveSuccess;

        public int KilledEnemies => killedEnemies;

        #region Unity Methods

        private void Awake()
        {
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += StartSpawning;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                StartSpawning(GameState.Wave);
            }
        }

        private void OnDestroy()
        {
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged -= StartSpawning;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Increases the amount of killed enemies and potentially ends the wave.
        /// </summary>
        public void IncreaseKilledEnemies()
        {
            killedEnemies++;

            if (KilledEnemies < waveSize)
                return;

            OnWaveSuccess?.Invoke(GameState.Build);
            killedEnemies = 0;
        }

        /// <summary>
        /// Updates the List of enemies.
        /// </summary>
        /// <param name="nextEnemies"> The new List of new enemies </param>
        public void UpdateNextEnemies(List<GameObject> nextEnemies)
        {
            _enemies = nextEnemies;
            waveSize = nextEnemies.Count;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Starts spawning the currently saved enemies.
        /// </summary>
        private void StartSpawning(GameState state)
        {
            if (state != GameState.Wave)
                return;

            if (_coroutineIsRunning)
                return;

            StartCoroutine(SpawnEnemies());
        }

        /// <summary>
        /// Updates the current spawn point.
        /// </summary>
        private void UpdateSpawnPoints()
        {
            if(spawnPositions.Count < 2)
                return;

            _currentSpawnPosition++;
            
            if (_currentSpawnPosition >= spawnPositions.Count)
                _currentSpawnPosition = 0;
        }

        /// <summary>
        /// Spawns a list of enemies with a small delay between each of them.
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpawnEnemies()
        {
            _coroutineIsRunning = true;

            foreach (GameObject enemy in _enemies)
            {
                UpdateSpawnPoints();
                var en = enemy.GetComponent<Enemy>();
                PhotonNetwork.Instantiate
                    (string.Concat(en.EnemyPath, en.EnemyName), spawnPositions[_currentSpawnPosition].position, Quaternion.identity);
                yield return new WaitForSeconds(enemySpawnDelay);
            }

            _coroutineIsRunning = false;
        }

        #endregion
    }
}