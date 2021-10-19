using System;
using System.Collections.Generic;
using _Project.GamePlay.GameManager.Scripts;
using UnityEngine;

namespace _Project.GamePlay.Spawning.WaveManager.Scripts
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private List<Wave.Scripts.Wave> waves = new List<Wave.Scripts.Wave>();
        [SerializeField] private int currentWave;

        public event Action<List<GameObject>> OnUpdateWave;

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
            OnUpdateWave?.Invoke(waves[currentWave] == null ? waves[0].Enemies : waves[currentWave].Enemies);
        }
    }
}