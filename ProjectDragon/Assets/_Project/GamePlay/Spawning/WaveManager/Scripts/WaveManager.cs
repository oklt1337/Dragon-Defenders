using System;
using System.Collections.Generic;
using _Project.AI.Enemies.Scripts;
using _Project.GamePlay.GameManager.Scripts;
using UnityEngine;

namespace _Project.GamePlay.Spawning.WaveManager.Scripts
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private Wave.Scripts.Wave currentWave;
        [SerializeField] private int currentWaveIndex;

        public event Action<List<Enemy>> OnUpdateWave;

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
            
            currentWaveIndex++;
            SetNewWave();
            OnUpdateWave?.Invoke(currentWave.Enemies);
        }

        /// <summary>
        /// Get new wave from WaveGenerator or set one manually
        /// </summary>
        private void SetNewWave()
        {
            currentWave = GameManager.Scripts.GameManager.Instance.WaveGenerator.GetNextWave(currentWave);
        }
    }
}