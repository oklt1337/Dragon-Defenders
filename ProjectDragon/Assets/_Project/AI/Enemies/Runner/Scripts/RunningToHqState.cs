using _Project.AI.FSM.Scripts;

namespace _Project.AI.Enemies.Runner.Scripts
{
    public class RunningToHqState : State
    {
        private Runner owner;
        
        public RunningToHqState(FiniteStateMachine finiteStateMachine, Runner newOwner) : base(finiteStateMachine)
        {
            owner = newOwner;
        }
        public override void CheckTransition() {}

        public override void OnEnter() {}

        public override void Update()
        {
            owner.WalkToHq();
        }

        public override void OnExit() {}
    }
}
