using UnityEngine;
using UnityEngine.AI;
using static _Project.Scripts.Gameplay.Enemies.EnemySpawner;
using static _Project.Scripts.Gameplay.GameManager;

namespace _Project.Scripts.Gameplay.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
       [SerializeField] private string enemyName;
       [SerializeField] private GameObject enemyModel;
       [SerializeField] private float health;
       [SerializeField] private float defense;
       [SerializeField] private float speed;
       [SerializeField] private float maxSpeed;
       [SerializeField] private float hqDamage;
       [SerializeField] private float expDrop;
       [SerializeField] private float goldDrop;
       [SerializeField] internal NavMeshAgent agent;

        public void TakeDamage(float damage)
        {
            if(damage < defense)
                return;
            
            health -= (damage - defense);
            
            if(health <= 0)
                Death();
        }

        private void Death()
        {
            Instance.EnemySpawner.IncreaseKilledEnemies();
            Destroy(this);
        }
    }
}
