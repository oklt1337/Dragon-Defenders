using AI.Enemies.Base_Enemy;
using AI.Enemies.Base_Enemy.Scripts;
using GamePlay.GameManager.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Enemies.Flying_Enemies.Base_Flying_Enemies.Scripts
{
    public class BaseFlyingEnemy : Enemy
    {
        [SerializeField] protected NavMeshAgent agent;

        public NavMeshAgent Agent => agent;

        private void Start()
        {
            agent.speed = speed;
        }

        /// <summary>
        /// Makes the Enemy fly to the Hq.
        /// </summary>
        public void FlyToHq()
        {
            agent.SetDestination(GameManager.Instance.Hq.Hq.transform.position);
        }
    }
}
