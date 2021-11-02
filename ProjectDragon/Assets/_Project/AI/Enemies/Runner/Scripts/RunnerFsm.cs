using _Project.AI.FSM.Scripts;

namespace _Project.AI.Enemies.Runner.Scripts
{
    public class RunnerFsm : FiniteStateMachine
    {
        private Runner owner;
        
        //States
        public State RunToHqState { get; }
        
        public RunnerFsm(Runner newOwner)
        {
            owner = newOwner;
            RunToHqState = new RunningToHqState(this, owner);
        }
    }
}
