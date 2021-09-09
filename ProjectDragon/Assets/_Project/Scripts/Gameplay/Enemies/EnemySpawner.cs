using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner EnemySpawnerInstance;
        
       [SerializeField] private int waveSize;
       [SerializeField] private int killedEnemies;

        public Action OnWaveSucces;

        public int KilledEnemies => killedEnemies;
        public int WaveSize => waveSize;

        private void Awake()
        {
            if (EnemySpawnerInstance != null)
            {
                Destroy(this);
                return;
            }

            EnemySpawnerInstance = this;
        }

        /// <summary>
        /// Increases the amount of killed enemies and potentially ends the wave.
        /// </summary>
        public void IncreaseKilledEnemies()
        {
            killedEnemies++;
            
            if(KilledEnemies >= WaveSize)
                OnWaveSucces?.Invoke();
        }
    }
}
