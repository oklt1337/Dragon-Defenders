using AI.Enemies.Flying_Enemies.Flyer.Scripts;
using AI.FSM.Scripts;

namespace AI.States.Base_Flying_Enemies
{
    public class FlyToHqState : State
    {
        private Flyer owner;
        
        public FlyToHqState(FiniteStateMachine finiteStateMachine, Flyer newOwner) : base(finiteStateMachine)
        {
            owner = newOwner;
        }
        
        public override void CheckTransition()
        {
            if (owner.IsStunned)
            {
                FiniteStateMachine.Transition(FiniteStateMachine.EndureStunState);
            }
        }

        public override void OnEnter()
        {
            
        }

        public override void Update()
        {
            owner.FlyToHq();
        }

        public override void OnExit()
        {
            
        }
    }
}