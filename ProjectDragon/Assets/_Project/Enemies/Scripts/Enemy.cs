using UnityEngine;
using UnityEngine.AI;
using static _Project.GamePlay.GameManager.Scripts.GameManager;

namespace _Project.Enemies.Scripts
{
    public abstract class Enemy : MonoBehaviour
    {
        #region Serialized Fields
        
       [SerializeField] private string enemyName;
       [SerializeField] private GameObject enemyModel;
       [SerializeField] private float defense;
       [SerializeField] private float maxSpeed;
       [SerializeField] private float hqDamage;
       [SerializeField] private float expDrop;
       [SerializeField] private int goldDrop;

        #endregion
        
        #region Protected Fields
        
       [SerializeField] protected NavMeshAgent agent;
       [SerializeField] protected float health;
       [SerializeField] protected float maxHealth;
       [SerializeField] protected float speed;

        #endregion
        

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
            Instance.PlayerModel.AddMoney(goldDrop);
            Destroy(this);
        }
    }
}
