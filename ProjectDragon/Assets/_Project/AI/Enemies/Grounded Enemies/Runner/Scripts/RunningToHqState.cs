using _Project.AI.FSM.Scripts;
using AI.Enemies.Grounded_Enemies.Base_Grounded_Enemies;
using AI.FSM.Scripts;
using GamePlay.GameManager.Scripts;

namespace AI.Enemies.Grounded_Enemies.Runner.Scripts
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
            if (owner is Runner runner)
            {
                
            }
        }

        public override void OnEnter()
        {
            // Only for Beta, will be updated for multiple splines.
            owner.SetSpline(GameManager.Instance.SplineComputer);
        }

        public override void Update()
        {
            owner.WalkToHq();
        }

        public override void OnExit()
        {
            
        }
    }
}
