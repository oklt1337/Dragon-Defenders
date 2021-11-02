using UnityEngine;
using static _Project.GamePlay.GameManager.Scripts.GameManager;

namespace _Project.AI.Enemies.Base_Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        #region Private Serialized Fields

        [Header("Internal Stats")]
        [SerializeField] private string enemyName;
        [SerializeField] private string enemyPath;
        [SerializeField] private int enemyCombatScore;
        
        [Header("Gameplay Stats")]
        [SerializeField] private float defense;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float expDrop;
        [SerializeField] private int goldDrop;
        [SerializeField] private int hqDamage;
        
        #endregion

        #region Protected Serialized Fields

        [SerializeField] protected float health;
        [SerializeField] protected float maxHealth;
        [SerializeField] protected float speed;

        #endregion

        #region Public Properties

        public string EnemyName => enemyName;
        public string EnemyPath => enemyPath;
        public int EnemyCombatScore => enemyCombatScore;

        #endregion

        #region Unity Methods
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("HQ"))
                return;

            Instance.Hq.Hq.TakeDamage(hqDamage);
            Death();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Deals damage to the enemy.
        /// </summary>
        /// <param name="damage"> How much damage the attack is dealing. </param>
        protected virtual void TakeDamage(float damage)
        {
            if (damage < defense)
                return;

            health -= (damage - defense);

            if (health <= 0)
                Death();
        }

        /// <summary>
        /// The enemies death.
        /// </summary>
        protected virtual void Death()
        {
            Instance.EnemySpawner.IncreaseKilledEnemies();
            Instance.PlayerModel.ModifyMoney(goldDrop);
            Destroy(gameObject);
        }
        
        #endregion
    }
}