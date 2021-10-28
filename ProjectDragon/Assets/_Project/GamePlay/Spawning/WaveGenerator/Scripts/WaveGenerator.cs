using System.Collections.Generic;
using System.Linq;
using _Project.AI.Enemies.Scripts;
using Sirenix.Serialization;
using UnityEngine;

namespace _Project.GamePlay.Spawning.WaveGenerator.Scripts
{
    public class WaveGenerator : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private List<Enemy> allEnemies = new List<Enemy>();
        [OdinSerialize] private Dictionary<Enemy, bool> allowedEnemies = new Dictionary<Enemy, bool>();
        [SerializeField] private int waveDifficultlyModifier;
        [SerializeField] private int strengthsThreshold;
        [SerializeField] private int minWaveCountModifier;
        [SerializeField] private int maxWaveCountModifier;

        #endregion

        #region Private Fields

        private int combatScore;
        private int minEnemies;
        private int maxEnemies;

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
        public Wave.Scripts.Wave GetNextWave(Wave.Scripts.Wave lastWave)
        {
            combatScore = lastWave.WaveCombatScore * waveDifficultlyModifier;
            
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
        private Wave.Scripts.Wave GenerateWave(IEnumerable<Enemy> enemies)
        {
            minEnemies = SetMinEnemiesCount(combatScore);
            maxEnemies = SetMaxEnemiesCount(combatScore);
            var enemyCount = Random.Range(minEnemies, maxEnemies);
            
            var wave = ScriptableObject.CreateInstance<Wave.Scripts.Wave>();
            var enemiesWithinCombatScore = enemies.Where(enemy => enemy.EnemyCombatScore <= combatScore).ToList();

            var averageOfAllEnemies = (float)  combatScore / enemyCount;
            
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
                    wave.Enemies.Add(lowerAverage[index]);
                    totalCurrentEnemyScore += lowerAverage[index].EnemyCombatScore;
                }
                else
                {
                    index = Random.Range(0, higherAverage.Count);
                    wave.Enemies.Add(higherAverage[index]);
                    totalCurrentEnemyScore += higherAverage[index].EnemyCombatScore;
                }
                
                averageOfCurrentEnemy = totalCurrentEnemyScore / wave.Enemies.Count;
            }
            
            return wave;
        }

        /// <summary>
        /// Sets Min enemies of wave.
        /// </summary>
        /// <param name="waveCombatScore">int</param>
        /// <returns>int minEnemies</returns>
        private int SetMinEnemiesCount(int waveCombatScore)
        {
            var newMinEnemies = waveCombatScore / minWaveCountModifier;
            const int min = 10;

            if (newMinEnemies <= min)
            {
                newMinEnemies = min;
            }

            return newMinEnemies;
        }

        /// <summary>
        /// Sets Max enemies of wave.
        /// </summary>
        /// <param name="waveCombatScore">int</param>
        /// <returns>int maxEnemies</returns>
        private int SetMaxEnemiesCount(int waveCombatScore)
        {
            var newMaxEnemies = waveCombatScore / maxWaveCountModifier;
            const int max = 100;

            if (newMaxEnemies <= max)
            {
                newMaxEnemies = max;
            }

            return newMaxEnemies;
        }

        #endregion
    }
}