using _Project.AI.FSM.Scripts;
using AI.FSM.Scripts;
using GamePlay.GameManager.Scripts;

namespace AI.Enemies.Grounded_Enemies.Runner.Scripts
{
    public class RunningToHqState : State
    {
        private Runner owner;
        
        public RunningToHqState(FiniteStateMachine finiteStateMachine, global::AI.Enemies.Grounded_Enemies.Runner.Scripts.Runner newOwner) : base(finiteStateMachine)
        {
            owner = newOwner;
        }
        public override void CheckTransition() {}

        public override void OnEnter()
        {
            // Only for Beta, will be done in the enemy spawner for multiple splines.
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
