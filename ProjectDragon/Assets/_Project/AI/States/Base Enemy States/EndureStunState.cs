using AI.Enemies.Base_Enemy;
using AI.Enemies.Flying_Enemies.Base_Flying_Enemies.Scripts;
using AI.Enemies.Flying_Enemies.Flyer.Scripts;
using AI.Enemies.Grounded_Enemies.Base_Grounded_Enemies.Scripts;
using AI.Enemies.Grounded_Enemies.Runner.Scripts;
using AI.FSM.Scripts;

namespace AI.States.Base_Enemy_States
{
    public class EndureStunState : State
    {
        private Enemy owner;

        public EndureStunState(FiniteStateMachine finiteStateMachine, Enemy newOwner) : base(finiteStateMachine)
        {
            owner = newOwner;
        }

        public override void CheckTransition()
        {
            if (owner.StunDuration > 0)
                return;

            if (owner is BaseGroundedEnemies)
            {
                if (owner is Runner runner)
                {
                    FiniteStateMachine.Transition(runner.Fsm.RunToHqState);
                }
            }
            else if (owner is BaseFlyingEnemy)
            {
                if (owner is Flyer flyer)
                {
                    FiniteStateMachine.Transition(flyer.Fsm.FlyToHqState);
                }
            }
        }

        public override void OnEnter()
        {
        }

        public override void Update()
        {
            owner.WaitForStun();
        }

        public override void OnExit()
        {
        }
    }
}