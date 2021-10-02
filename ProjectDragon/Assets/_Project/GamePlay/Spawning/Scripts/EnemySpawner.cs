using System;
using System.Collections;
using System.Collections.Generic;
using _Project.AI.Enemies.Scripts;
using UnityEngine;

namespace _Project.GamePlay.Spawning.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
       [SerializeField] private int waveSize;
       [SerializeField] private int killedEnemies;

       public Action OnWaveSucces;

       public GameObject Mover;
       
        public int KilledEnemies => killedEnemies;
        public int WaveSize => waveSize;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Instantiate(Mover, transform.position, Quaternion.identity);
            }
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

        public IEnumerator SpawnEnemies(List<Enemy> enemies)
        {
            return null;
        }
    }
}
