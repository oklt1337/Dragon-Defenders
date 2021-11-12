using AI.Enemies.Grounded_Enemies.Base_Grounded_Enemies.Scripts;
using AI.Enemies.Grounded_Enemies.Grounded_Attacker.Scripts;
using AI.FSM.Scripts;
using GamePlay.GameManager.Scripts;
using UnityEngine;

namespace AI.States.Base_Grounded_Enemies_States
{
    public class RunningToHqState : State
    {
        private BaseGroundedEnemies owner;
        
        public RunningToHqState(FiniteStateMachine finiteStateMachine, BaseGroundedEnemies newOwner) : base(finiteStateMachine)
        {
            owner = newOwner;
        }

        public override void CheckTransition()
        {
            
        }

        public override void OnEnter()
        {
            owner.Follower.follow = true;
        }

        public override void Update()
        {
            owner.WalkToHq();
        }

        public override void OnExit()
        {
            owner.Follower.follow = false;
        }
    }
}
