using AI.Enemies.Grounded_Enemies.Base_Grounded_Enemies;
using AI.Enemies.Grounded_Enemies.Base_Grounded_Enemies.Scripts;
using AI.FSM.Scripts;
using AI.States.Base_Enemy_States;
using GamePlay.GameManager.Scripts;

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
            
            // Only for Beta, will be updated for multiple splines.
            owner.Follower.spline = GameManager.Instance.SplineComputer;
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
