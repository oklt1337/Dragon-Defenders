using System;
using _Project.AI.Enemies.Base_Enemy;
using _Project.GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace _Project.AI.Enemies.Scripts
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
