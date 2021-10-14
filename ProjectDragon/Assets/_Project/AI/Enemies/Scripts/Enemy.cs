using _Project.GamePlay.GameManager.Scripts;
using UnityEngine;
using UnityEngine.AI;
using static _Project.GamePlay.GameManager.Scripts.GameManager;

namespace _Project.AI.Enemies.Scripts
{
    public abstract class Enemy : MonoBehaviour
    {
        #region Serialized Fields
        
       [SerializeField] private string enemyName;
       [SerializeField] private GameObject enemyModel;
       [SerializeField] private float defense;
       [SerializeField] private float maxSpeed;
       [SerializeField] private float expDrop;
       [SerializeField] private int goldDrop;
       [SerializeField] private int hqDamage;

        #endregion
        
        #region Protected Fields
        
       [SerializeField] protected NavMeshAgent agent;
       [SerializeField] protected float health;
       [SerializeField] protected float maxHealth;
       [SerializeField] protected float speed;

        #endregion
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("HQ")) 
                return;
            
            Instance.Hq.Hq.TakeDamage(hqDamage);
            Death();
        }

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
            Instance.PlayerModel.ModifyMoney(goldDrop);
            Destroy(gameObject);
        }
    }
}
