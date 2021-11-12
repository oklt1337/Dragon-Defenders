using AI.Enemies.Grounded_Enemies.Grounded_Attacker.Scripts;
using AI.FSM.Scripts;
using GamePlay.GameManager.Scripts;
using UnityEngine;

namespace AI.States.Grounded_Attacker_States
{
    public class AttackingState : State
    {
        private GroundedAttacker owner;
    
        public AttackingState(FiniteStateMachine finiteStateMachine, GroundedAttacker newOwner) : base(finiteStateMachine)
        {
            owner = newOwner;
        }

        public override void CheckTransition()
        {
           
        }

        public override void OnEnter()
        {
        
        }

        public override void Update()
        {
            owner.Attack();
        }

        public override void OnExit()
        {
        }
    }
}
