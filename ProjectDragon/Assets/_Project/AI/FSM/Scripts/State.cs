using _Project.AI.Enemies.Base_Enemy;
using _Project.AI.Enemies.Scripts;

namespace _Project.AI.FSM.Scripts
{
    public abstract class State
    {
        protected readonly FiniteStateMachine FiniteStateMachine;
        protected readonly Enemy Owner;

        protected State(FiniteStateMachine finiteStateMachine, Enemy owner)
        {
            FiniteStateMachine = finiteStateMachine;
            Owner = owner;
        }

        public abstract void CheckTransition();

        public abstract void OnEnter();

        public abstract void Update();
        
        public abstract void OnExit();
    }
}
