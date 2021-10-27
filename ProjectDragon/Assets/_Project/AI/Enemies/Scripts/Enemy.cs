using UnityEngine;
using UnityEngine.AI;
using static _Project.GamePlay.GameManager.Scripts.GameManager;

namespace _Project.AI.Enemies.Scripts
{
    public abstract class Enemy : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private string enemyName;
        [SerializeField] private string enemyPath;
        [SerializeField] private float defense;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float expDrop;
        [SerializeField] private int goldDrop;
        [SerializeField] private int hqDamage;
        [SerializeField] private int enemyCombatScore;

        [SerializeField] private GameObject enemyModel;
        #endregion

        #region Protected Fields

        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected float health;
        [SerializeField] protected float maxHealth;
        [SerializeField] protected float speed;

        #endregion

        #region Public Fields

        public string EnemyName
        {
            get => enemyName;
            set => enemyName = value;
        }

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