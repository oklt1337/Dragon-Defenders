using System;
using Abilities.Ability.Scripts;
using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.Projectiles.Scripts;
using AI.Enemies.Grounded_Enemies.Base_Grounded_Enemies.Scripts;
using GamePlay.GameManager.Scripts;
using UnityEngine;

namespace AI.Enemies.Grounded_Enemies.Grounded_Attacker.Scripts
{
    public class GroundedAttacker : BaseGroundedEnemies
    {
        public GroundedAttackerFsm Fsm { get; private set; }

        [SerializeField] private float attackRange;
        [SerializeField] private Transform abilitySpawnPosition;
        
        [SerializeField] private SingleShotAbilityObj attackObj;
        private SingleShotAbility attack;

        public float AttackRange => attackRange;
        
        #region Unity Methods

        private void Awake()
        {
            Fsm = new GroundedAttackerFsm(this);

            Fsm.Initialize(Fsm.RunToHqState);

            attack = attackObj.CreateInstance();
            
            attackRange = attack.AttackRange;
        }

        private void FixedUpdate()
        {
            Fsm.Update();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
                Fsm.Transition(Fsm.AttackState);
            
            base.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
                Fsm.Transition(Fsm.RunToHqState);
        }

        #endregion

        /// <summary>
        /// Uses his damage ability to attack the commander.
        /// </summary>
        public void Attack()
        {
            if (attack == null)
                return;

            attack.Cast(abilitySpawnPosition, GameManager.Instance.PlayerModel.Commander.transform, Caster.Enemy);
            
            if (!attack.Casted && !(attack.TimeLeft > 0)) 
                return;
            
            attack.Tick(Time.deltaTime);
        }
        
        public override void Stun(float stunTime)
        {
            base.Stun(stunTime);
            Fsm.Transition(Fsm.EndureStunState);
        }
    }
}
