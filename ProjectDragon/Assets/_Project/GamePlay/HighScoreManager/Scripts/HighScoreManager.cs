using AI.Enemies.Base_Enemy.Scripts;
using UnityEngine;

namespace GamePlay.HighScoreManager.Scripts
{
    public class HighScoreManager : MonoBehaviour
    {
        private float highScore;
        public float HighScore => highScore;

        private void Awake()
        {
            GameManager.Scripts.GameManager.Instance.EnemySpawner.OnEnemySpawn += WatchEnemy;
        }

        private void WatchEnemy(Enemy enemy)
        {
            enemy.OnDeath += (b =>
            {
                AddScore(b, enemy);
            });
        }

        private void AddScore(bool isPlayer, Enemy enemy)
        {
            if (isPlayer)
            {
                highScore += enemy.EnemyCombatScore;
            }
        }
    }
}