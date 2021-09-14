namespace _Project.Scripts.Gameplay.Enemies.EnemyBehaviourTree
{
    public abstract class Node
    {
        protected NodeState CurrentState;

        public abstract NodeState Evaluate();
    
        public virtual void Reset(){}
    
        public enum NodeState
        {
            Success,
            Failure,
            Working
        }
    }
}
