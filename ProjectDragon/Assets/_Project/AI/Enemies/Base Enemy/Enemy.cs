using UnityEngine;
using static _Project.GamePlay.GameManager.Scripts.GameManager;

namespace _Project.AI.Enemies.Base_Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        #region Serialized Fields

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

        #region Protected Fields

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

        #region Public Methods

        public virtual void TakeDamage(float damage)
        {
            if (damage < defense)
                return;

            health -= (damage - defense);

            if (health <= 0)
                Death();
        }

        #endregion

        #region Private Methods

        protected virtual void Death()
        {
            Instance.EnemySpawner.IncreaseKilledEnemies();
            Instance.PlayerModel.ModifyMoney(goldDrop);
            Destroy(gameObject);
        }

        #endregion
    }
}