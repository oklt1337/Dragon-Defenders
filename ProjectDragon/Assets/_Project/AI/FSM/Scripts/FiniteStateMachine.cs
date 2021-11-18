using AI.Enemies.Base_Enemy.Scripts;
using AI.States.Base_Enemy_States;

namespace AI.FSM.Scripts
{
    public abstract class FiniteStateMachine
    {
        public State CurrentState { get; private set; }

        public State EndureStunState { get; private set; }

        protected FiniteStateMachine(Enemy newOwner)
        {
            EndureStunState = new EndureStunState(this, newOwner);
        }

        #region Public Methods

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
            CurrentState.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }

        #endregion
    }
}