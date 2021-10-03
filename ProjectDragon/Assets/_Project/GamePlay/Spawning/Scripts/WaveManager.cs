using System.Collections.Generic;
using UnityEngine;

namespace _Project.GamePlay.Spawning.Scripts
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private List<Wave> waves = new List<Wave>();
        [SerializeField] private int currentWave;

        private void Awake()
        {
            GameManager.Scripts.GameManager.Instance.EnemySpawner.OnWaveSucces += AdvanceToNextWave;
        }

        /// <summary>
        /// Increases the wave count.
        /// </summary>
        private void AdvanceToNextWave()
        {
            currentWave++;
        }

        /// <summary>
        ///  Tells the enemy spawner what to spawn.
        /// </summary>
        public void SpawnNextWave()
        {
           StartCoroutine(GameManager.Scripts.GameManager.Instance.EnemySpawner.SpawnEnemies(waves[currentWave].Enemies));
        }
    }
}
