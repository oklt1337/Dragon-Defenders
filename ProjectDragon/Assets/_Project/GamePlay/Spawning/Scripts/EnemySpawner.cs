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
       [SerializeField] private float enemySpawnDelay;

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

        /// <summary>
        /// Spawns a list of enemies with a small delay between each of them.
        /// </summary>
        /// <param name="enemies"></param>
        /// <returns></returns>
        public IEnumerator SpawnEnemies(List<GameObject> enemies)
        {
            foreach (var enemy in enemies)
            {
                Instantiate(enemy, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(enemySpawnDelay);
            }
        }
    }
}
