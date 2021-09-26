using System.Collections.Generic;
using UnityEngine;

namespace _Project.GamePlay.Spawning.Scripts
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private List<Wave> waves = new List<Wave>();

        private int _currentWave;

        private void Awake()
        {
            GameManager.Scripts.GameManager.Instance.EnemySpawner.OnWaveSucces += AdvanceToNextWave;
        }

        // Increases the wave count and tells the enemy spawner what to spawn.
        private void AdvanceToNextWave()
        {
            _currentWave++;
            GameManager.Scripts.GameManager.Instance.EnemySpawner.SpawnEnemies(waves[_currentWave].Enemies);
        }
    }
}
