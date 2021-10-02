using _Project.GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace _Project.AI.Enemies.Scripts
{
    public class Attacker : Enemy
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float attackDamageModifier;
        // [SerializeField] private Attack attack;
        [SerializeField] private Commander target;

        public float AttackRange => attackRange;

        public Commander Target => target;
        
        public void WalkToCommander()
        {
            agent.SetDestination(target.transform.position);
        }
        
        public void FlyToCommander()
        {
            agent.SetDestination(target.transform.position);
        }

        public void Attack()
        {
            
        }
    }
}
