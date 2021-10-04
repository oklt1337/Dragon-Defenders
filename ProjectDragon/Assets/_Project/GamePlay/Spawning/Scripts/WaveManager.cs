using System.Collections.Generic;
using _Project.GamePlay.GameManager.Scripts;
using UnityEngine;

namespace _Project.GamePlay.Spawning.Scripts
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private List<Wave> waves = new List<Wave>();
        [SerializeField] private int currentWave;

        private void Awake()
        {
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += AdvanceToNextWave;
        }
        
        private void OnDestroy()
        {
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged -= AdvanceToNextWave;
        }

        /// <summary>
        /// Increases the wave count and updates the enemy spawners next enemies.
        /// </summary>
        private void AdvanceToNextWave(GameState state)
        {
            if(state != GameState.Build)
                return;
            
            currentWave++;
            // For the Alpha we only use one wave.
            //GameManager.Scripts.GameManager.Instance.EnemySpawner.UpdateNextEnemies(waves[currentWave].Enemies);
            GameManager.Scripts.GameManager.Instance.EnemySpawner.UpdateNextEnemies(waves[0].Enemies);
        }
    }
}