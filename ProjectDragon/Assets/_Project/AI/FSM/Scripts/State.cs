namespace _Project.AI.FSM.Scripts
{
    public abstract class State
    {
        protected readonly FiniteStateMachine FiniteStateMachine;

        protected State(FiniteStateMachine finiteStateMachine)
        {
            FiniteStateMachine = finiteStateMachine;
        }

        public abstract void CheckTransition();

        public abstract void OnEnter();

        public abstract void Update();
        
        public abstract void OnExit();
    }
}
