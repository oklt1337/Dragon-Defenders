using _Project.AI.Enemies.Base_Enemy;
using _Project.AI.Enemies.Scripts;

namespace _Project.AI.FSM.Scripts
{
    public class FiniteStateMachine
    {
        public State CurrentState { get; private set; }

        //States
       // public State IdleState { get; }
       // public State AlertState { get; }
        public State RunAtTargetState { get; }
       // public State AttackState { get; }
       // public State BreathState { get; }
        
        public FiniteStateMachine(Enemy owner)
        {
           
        }

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
