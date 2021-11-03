using _Project.AI.FSM.Scripts;

namespace AI.Enemies.Runner.Scripts
{
    public class RunnerFsm : FiniteStateMachine
    {
        private global::AI.Enemies.Runner.Scripts.Runner owner;
        
        //States
        public State RunToHqState { get; }
        
        public RunnerFsm(global::AI.Enemies.Runner.Scripts.Runner newOwner)
        {
            owner = newOwner;
            RunToHqState = new RunningToHqState(this, owner);
        }
    }
}
