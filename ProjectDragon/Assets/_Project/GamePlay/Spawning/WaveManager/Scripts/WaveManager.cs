using System;
using System.Collections.Generic;
using AI.Enemies.Base_Enemy;
using AI.Enemies.Base_Enemy.Scripts;
using GamePlay.GameManager.Scripts;
using UnityEngine;

namespace GamePlay.Spawning.WaveManager.Scripts
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private _Project.GamePlay.Spawning.Wave.Scripts.Wave currentWave;
        
        public int CurrentWaveIndex { get; private set; }

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
            
            CurrentWaveIndex++;
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