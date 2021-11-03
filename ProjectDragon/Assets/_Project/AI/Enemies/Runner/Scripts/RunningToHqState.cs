using _Project.AI.FSM.Scripts;

namespace AI.Enemies.Runner.Scripts
{
    public class RunningToHqState : State
    {
        private global::AI.Enemies.Runner.Scripts.Runner owner;
        
        public RunningToHqState(FiniteStateMachine finiteStateMachine, global::AI.Enemies.Runner.Scripts.Runner newOwner) : base(finiteStateMachine)
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
