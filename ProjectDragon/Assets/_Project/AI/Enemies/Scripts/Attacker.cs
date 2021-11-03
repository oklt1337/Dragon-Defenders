using AI.Enemies.Base_Enemy;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace AI.Enemies.Scripts
{
    public class Attacker : Enemy
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float attackDamageModifier;
        // [SerializeField] private Attack attack;
        [SerializeField] protected Commander target;

        public float AttackRange => attackRange;

        public Commander Target => target;

        public void Attack()
        {
            
        }

        private void Start()
        {
            
        }
    }
}
