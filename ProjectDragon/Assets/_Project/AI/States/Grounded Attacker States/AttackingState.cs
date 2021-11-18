using AI.Enemies.Grounded_Enemies.Grounded_Attacker.Scripts;
using AI.FSM.Scripts;
using UnityEngine;

namespace AI.States.Grounded_Attacker_States
{
    public class AttackingState : State
    {
        private readonly GroundedAttacker owner;
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

        public AttackingState(FiniteStateMachine finiteStateMachine, GroundedAttacker newOwner) : base(finiteStateMachine)
        {
            owner = newOwner;
        }

        public override void CheckTransition()
        {
           
        }

        public override void OnEnter()
        {
            owner.Animator.SetBool(IsAttacking, true);
        }

        public override void Update()
        {
            owner.Attack();
        }

        public override void OnExit()
        {
            owner.Animator.SetBool(IsAttacking, false);
        }
    }
}
