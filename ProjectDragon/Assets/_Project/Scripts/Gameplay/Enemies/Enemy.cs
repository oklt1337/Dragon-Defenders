using System;
using UnityEngine;
using UnityEngine.AI;
using static _Project.GamePlay.GameManager.Scripts.GameManager;

namespace _Project.Scripts.Gameplay.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
       [SerializeField] private string enemyName;
       [SerializeField] private GameObject enemyModel;
       [SerializeField] protected float health;
       [SerializeField] protected float maxHealth;
       [SerializeField] private float defense;
       [SerializeField] protected float speed;
       [SerializeField] private float maxSpeed;
       [SerializeField] private float hqDamage;
       [SerializeField] private float expDrop;
       [SerializeField] private float goldDrop;
       [SerializeField] protected NavMeshAgent agent;

        public virtual void TakeDamage(float damage)
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
            // Instance.PlayerModel.AddMoney(goldDrop);
            Destroy(this);
        }
    }
}
