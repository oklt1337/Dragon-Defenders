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
        [SerializeField] private int scoreDecreaseValue;
        [SerializeField] private int strengthsThreshold;
        [SerializeField] private int minWaveCountModifier;
        [SerializeField] private int maxWaveCountModifier;

        #endregion

        #region MyRegion

        private int combatScore;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            foreach (var enemy in allEnemies)
            {
                allowedEnemies.Add(enemy, enemy.EnemyCombatScore > strengthsThreshold);
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
        private Wave.Scripts.Wave GenerateWave(List<Enemy> enemies)
        {
            var wave = ScriptableObject.CreateInstance<Wave.Scripts.Wave>();
            
            while (!ValidateWave(wave))
            {
                //Generate new Wave

            }
            return wave;
        }

        /// <summary>
        /// Validates a wave.
        /// </summary>
        /// <param name="wave">Wave</param>
        /// <returns>bool = true if wave is valid.</returns>
        private bool ValidateWave(Wave.Scripts.Wave wave)
        {
            var minEnemies = SetMinEnemiesCount(wave.WaveCombatScore);
            var maxEnemies = SetMaxEnemiesCount(wave.WaveCombatScore);

            if (wave == null)
                return false;

            //Wave Rules
            //Wave must contain min X enemies
            //Wave cant contain more then X enemies
            if (wave.Enemies.Count > maxEnemies)
            {
                // decrease combatScore
                combatScore -= scoreDecreaseValue;
                return false;
            }

            if (wave.Enemies.Count >= minEnemies) 
                return true;
            
            // increase combatScore
            combatScore += scoreDecreaseValue;
            return false;
        }

        private int SetMinEnemiesCount(int waveCombatScore)
        {
            var minEnemies = waveCombatScore / minWaveCountModifier;
            const int min = 10;
            
            if (minEnemies <= min)
            {
                minEnemies = min;
            }

            return minEnemies;
        }
        
        private int SetMaxEnemiesCount(int waveCombatScore)
        {
            var maxEnemies = waveCombatScore / maxWaveCountModifier;
            const int max = 100;
            
            if (maxEnemies <= max)
            {
                maxEnemies = max;
            }

            return maxEnemies;
        }

        #endregion
    }
}
