using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Enemies.Scripts;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
       [SerializeField] private int waveSize;
       [SerializeField] private int killedEnemies;

        public Action OnWaveSucces;

        public int KilledEnemies => killedEnemies;
        public int WaveSize => waveSize;
        
        /// <summary>
        /// Increases the amount of killed enemies and potentially ends the wave.
        /// </summary>
        public void IncreaseKilledEnemies()
        {
            killedEnemies++;
            
            if(KilledEnemies >= WaveSize)
                OnWaveSucces?.Invoke();
        }

        public IEnumerator SpawnEnemies(List<Enemy> enemies)
        {
            return null;
        }
    }
}
