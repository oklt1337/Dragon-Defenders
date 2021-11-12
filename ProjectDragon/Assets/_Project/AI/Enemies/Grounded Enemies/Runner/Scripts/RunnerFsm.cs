using AI.FSM.Scripts;
using AI.States.Base_Grounded_Enemies_States;

namespace AI.Enemies.Grounded_Enemies.Runner.Scripts
{
    public class RunnerFsm : FiniteStateMachine
    {
        private Runner owner;
        
        // States.
        public State RunToHqState { get; }
        
        public RunnerFsm(Runner newOwner) : base(newOwner)
        {
            owner = newOwner;
            RunToHqState = new RunningToHqState(this, owner);
        }
    }
}
