using System.Collections.Generic;
using System.Linq;
using AI.Enemies.Base_Enemy.Scripts;
using Sirenix.Serialization;
using UnityEngine;
using _Project.GamePlay.Spawning.Wave.Scripts;

namespace GamePlay.Spawning.WaveGenerator.Scripts
{
    public class WaveGenerator : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private List<Enemy> allEnemies = new List<Enemy>();
        [OdinSerialize] private Dictionary<Enemy, bool> allowedEnemies = new Dictionary<Enemy, bool>();
        [SerializeField] private float waveDifficultlyModifier;
        [SerializeField] private int strengthsThreshold = 15;

        #endregion

        #region Private Fields

        private int combatScore;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            foreach (var enemy in allEnemies)
            {
                allowedEnemies.Add(enemy, enemy.EnemyCombatScore < strengthsThreshold);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set a new Enemy to allowed.
        /// </summary>
        /// <param name="enemy">Enemy new allowed enemy</param>
        public void SetNewEnemyActive(Enemy enemy)
        {
            if (allowedEnemies.ContainsKey(enemy))
            {
                allowedEnemies[enemy] = true;
            }
        }

        /// <summary>
        /// Returns Next Wave
        /// </summary>
        /// <param name="lastWave">Wave</param>
        /// <returns>Wave</returns>
        public _Project.GamePlay.Spawning.Wave.Scripts.Wave GetNextWave(_Project.GamePlay.Spawning.Wave.Scripts.Wave lastWave)
        {
            combatScore = (int) (lastWave.WaveCombatScore * waveDifficultlyModifier);
            var allowedEnemiesInWave = (from enemy in allowedEnemies where enemy.Value select enemy.Key).ToList();

            return GenerateWave(allowedEnemiesInWave);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generate a new Wave that is valid
        /// </summary>
        /// <param name="enemies">List Enemy</param>
        /// <returns>Wave</returns>
        private Wave GenerateWave(IEnumerable<Enemy> enemies)
        {
            var wave = ScriptableObject.CreateInstance<Wave>();
            var enemiesWithinCombatScore = enemies.Where(enemy => enemy.EnemyCombatScore <= combatScore).ToList();
            
            //Get Min and Max enemies
            var minEnemies = SetEnemiesCount(combatScore, 5);
            var maxEnemies = SetEnemiesCount(combatScore, 10);
            //Generate count of enemies
            var enemyCount = Random.Range(minEnemies, maxEnemies);
            
            //Get Avrg Combat Score of all enemies
            var averageOfAllEnemies = combatScore / enemyCount;
            if (averageOfAllEnemies == 0)
                averageOfAllEnemies = 1;

            //Sort enemies in List of lower and higher than the avg
            var lowerAverage = enemiesWithinCombatScore.Where(enemy => enemy.EnemyCombatScore <= averageOfAllEnemies).ToList();
            var higherAverage = enemiesWithinCombatScore.Where(enemy => enemy.EnemyCombatScore > averageOfAllEnemies).ToList();

            var averageOfCurrentEnemy = 0;
            var totalCurrentEnemyScore = 0;

            //Generate new Wave 
            for (var i = 0; i < enemyCount; i++)
            {
                int index;
                if (averageOfCurrentEnemy >= averageOfAllEnemies)
                { 
                    index = Random.Range(0, lowerAverage.Count);

                    if (lowerAverage.Count > index)
                    {
                        wave.Enemies.Add(lowerAverage[index]);
                        totalCurrentEnemyScore += lowerAverage[index].EnemyCombatScore;
                    }
                    else
                    {
                        index = Random.Range(0, higherAverage.Count);
                        wave.Enemies.Add(higherAverage[index]);
                        totalCurrentEnemyScore += higherAverage[index].EnemyCombatScore;
                    }
                }
                else
                {
                    index = Random.Range(0, higherAverage.Count);
                    if (higherAverage.Count > index)
                    {
                        wave.Enemies.Add(higherAverage[index]);
                        totalCurrentEnemyScore += higherAverage[index].EnemyCombatScore;
                    }
                    else
                    {
                        index = Random.Range(0, lowerAverage.Count);
                        wave.Enemies.Add(lowerAverage[index]);
                        totalCurrentEnemyScore += lowerAverage[index].EnemyCombatScore;
                    }
                }
                
                averageOfCurrentEnemy = totalCurrentEnemyScore / wave.Enemies.Count;
            }
            
            return wave;
        }

        /// <summary>
        /// Set enemy count.
        /// </summary>
        /// <param name="waveCombatScore">int</param>
        /// <param name="startEnemy">int</param>
        /// <returns>int enemy count</returns>
        private int SetEnemiesCount(int waveCombatScore, int startEnemy)
        {
            const int cMaxEnemiesThreshold = 20;
            if (waveCombatScore < cMaxEnemiesThreshold)
                return startEnemy;
            
            var threshold = (int) (cMaxEnemiesThreshold * 2.5f);
            if (waveCombatScore < threshold)
                return (int) (startEnemy * waveDifficultlyModifier);

            //Get new threshold and set new min
            var newMax = (int) (startEnemy * waveDifficultlyModifier);
            threshold = (int) (threshold * 2.5f);
            
            //increase threshold until waveCombatScore < threshold
            var index = 0;
            while (waveCombatScore > threshold)
            {
                index++;
                threshold = (int) (threshold * 2.5f);
            }
            
            if (index <= 0) 
                return newMax;
            
            //increase new min as often as threshold was increased.
            for (var i = 0; i < index; i++)
                newMax = (int) (newMax * waveDifficultlyModifier);
            
            return newMax;
        }

        #endregion
    }
}