namespace _Project.Enemies.Scripts.EnemyBehaviourTree
{
    public class FlyToCommander : Node
    {
        private Attacker _owner;

        public FlyToCommander(Attacker newOwner)
        {
            _owner = newOwner;
        }

        public override NodeState Evaluate()
        {
            float distance = (_owner.transform.position - _owner.Target.transform.position).magnitude;
            
            if (_owner.AttackRange > distance)
                return NodeState.Success;
            
            _owner.FlyToCommander();
            
            return NodeState.Working;
        }
    }
}
