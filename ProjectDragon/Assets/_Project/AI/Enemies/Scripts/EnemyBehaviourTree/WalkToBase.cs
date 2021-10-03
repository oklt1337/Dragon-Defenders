namespace _Project.AI.Enemies.Scripts.EnemyBehaviourTree
{
    public class WalkToBase : Node
    {
        private Runner _owner;

        public WalkToBase(Runner newOwner)
        {
            _owner = newOwner;
        }

        public override NodeState Evaluate()
        {
            _owner.WalkToBase();
            return NodeState.Working;
        }
    }
}
