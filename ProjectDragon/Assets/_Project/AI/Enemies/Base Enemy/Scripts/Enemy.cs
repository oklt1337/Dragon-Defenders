using UnityEngine;
using static GamePlay.GameManager.Scripts.GameManager;

namespace AI.Enemies.Base_Enemy.Scripts
{
    public abstract class Enemy : MonoBehaviour
    {
        #region Private Fields

        private float stunDuration;

        #endregion
        
        #region Private Serialized Fields

        [Header("Internal Stats")]
        [SerializeField] private string enemyName;
        [SerializeField] private string enemyPath;
        [SerializeField] private int enemyCombatScore;
        [SerializeField] private Animator animator;

        [Header("Gameplay Stats")] 
        [SerializeField] private float defense;
        [SerializeField] private float expDrop;
        [SerializeField] private int goldDrop;
        [SerializeField] private int hqDamage;

        #endregion

        #region Protected Serialized Fields

        [SerializeField] protected float health;
        [SerializeField] protected float maxHealth;
        [SerializeField] protected float speed;
        [SerializeField] protected float maxSpeed;

        #endregion

        #region Public Properties

        public float EnemyHealth => health;
        public float StunDuration => stunDuration;
        public string EnemyName => enemyName;
        public string EnemyPath => enemyPath;
        public int EnemyCombatScore => enemyCombatScore;
        public Animator Animator => animator;

        #endregion

        #region Unity Methods

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("HQ"))
                return;

            Instance.Hq.Hq.TakeDamage(hqDamage);
            Death(false);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// The enemies death.
        /// </summary>
        protected virtual void Death(bool byPlayer)
        {
            if (byPlayer)
            {
                Instance.PlayerModel.ModifyMoney(goldDrop);
                Instance.PlayerModel.Commander.AddExp(expDrop);
            }
            
            Instance.EnemySpawner.IncreaseKilledEnemies();
            Destroy(gameObject);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deals damage to the enemy.
        /// </summary>
        /// <param name="damage"> How much damage the attack is dealing. </param>
        public virtual void TakeDamage(float damage)
        {
            if (damage < defense)
                return;

            health -= (damage - defense);

            if (health <= 0)
                Death(true);
        }
        
        public virtual void Stun(float stunTime)
        {
            stunDuration = stunTime;
        }

        /// <summary>
        /// Reduces the Stun Duration continually.
        /// </summary>
        public void WaitForStun()
        {
            stunDuration -= Time.deltaTime;
        }

        #endregion
    }
}