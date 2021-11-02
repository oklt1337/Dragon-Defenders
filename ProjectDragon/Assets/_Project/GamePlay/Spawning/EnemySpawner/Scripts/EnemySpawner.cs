using System;
using System.Collections;
using System.Collections.Generic;
using _Project.AI.Enemies.Base_Enemy;
using _Project.AI.Enemies.Scripts;
using _Project.GamePlay.GameManager.Scripts;
using Photon.Pun;
using UnityEngine;

namespace _Project.GamePlay.Spawning.EnemySpawner.Scripts
{
    public class EnemySpawner : MonoBehaviourPun
    {
        [Header("Enemy Related Stuff")] 
        [SerializeField] private int waveSize;

        [SerializeField] private int killedEnemies;
        [SerializeField] private float enemySpawnDelay;

        private List<Enemy> enemies;

        [Header("Object Related Stuff")] 
        [SerializeField] private List<Transform> spawnPositions;

        private int currentSpawnPosition = 0;
        private bool coroutineIsRunning;

        public event Action<GameState> OnWaveSuccess;

        public int KilledEnemies => killedEnemies;

        #region Unity Methods

        private void Awake()
        {
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += StartSpawning;
            GameManager.Scripts.GameManager.Instance.WaveManager.OnUpdateWave += UpdateNextEnemies;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                StartSpawning(GameState.Wave);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                OnWaveSuccess?.Invoke(GameState.Prepare);
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

            OnWaveSuccess?.Invoke(GameState.Prepare);
            killedEnemies = 0;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the List of enemies.
        /// </summary>
        /// <param name="nextEnemies"> The new List of new enemies </param>
        private void UpdateNextEnemies(List<Enemy> nextEnemies)
        {
            enemies = nextEnemies;
            waveSize = nextEnemies.Count;
        }

        /// <summary>
        /// Starts spawning the currently saved enemies.
        /// </summary>
        private void StartSpawning(GameState state)
        {
            if (state != GameState.Wave)
                return;

            if (coroutineIsRunning)
                return;

            StartCoroutine(SpawnEnemies());
        }

        /// <summary>
        /// Updates the current spawn point.
        /// </summary>
        private void UpdateSpawnPoints()
        {
            if (spawnPositions.Count < 2)
                return;

            currentSpawnPosition++;

            if (currentSpawnPosition >= spawnPositions.Count)
                currentSpawnPosition = 0;
        }

        /// <summary>
        /// Spawns a list of enemies with a small delay between each of them.
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpawnEnemies()
        {
            coroutineIsRunning = true;

            foreach (var enemy in enemies)
            {
                UpdateSpawnPoints();
                PhotonNetwork.Instantiate(string.Concat(enemy.EnemyPath, enemy.EnemyName),
                    spawnPositions[currentSpawnPosition].position, Quaternion.identity);
                yield return new WaitForSeconds(enemySpawnDelay);
            }

            coroutineIsRunning = false;
        }

        #endregion
    }
}