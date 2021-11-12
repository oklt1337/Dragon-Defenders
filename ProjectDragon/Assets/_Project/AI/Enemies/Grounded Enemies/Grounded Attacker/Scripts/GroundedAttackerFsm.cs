using AI.FSM.Scripts;
using AI.States.Base_Grounded_Enemies_States;
using AI.States.Grounded_Attacker_States;

namespace AI.Enemies.Grounded_Enemies.Grounded_Attacker.Scripts
{
    public class GroundedAttackerFsm : FiniteStateMachine
    {
        private GroundedAttacker owner;
        
        // States.
        public State RunToHqState { get; }
        public State AttackState { get; }
        
        public GroundedAttackerFsm(GroundedAttacker newOwner) : base(newOwner)
        {
            owner = newOwner;
            RunToHqState = new RunningToHqState(this, owner);
            AttackState = new AttackingState(this, owner);
        }
    }
}
