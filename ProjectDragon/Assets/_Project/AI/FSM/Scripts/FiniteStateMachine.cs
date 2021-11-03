using _Project.AI.FSM.Scripts;

namespace AI.FSM.Scripts
{
    public abstract class FiniteStateMachine
    {
        public State CurrentState { get; private set; }
        
        public void Initialize(State initialState)
        {
            CurrentState = initialState;
            
            CurrentState.OnEnter();
        }

        public void Update()
        {
            CurrentState.CheckTransition();
            
            CurrentState.Update();
        }

        public void Transition(State newState)
        {
            
        }
    }
}
