namespace _Project.Enemies.Scripts.EnemyBehaviourTree
{
    public class WalkToCommander : Node
    {
        private Attacker _owner;
        public WalkToCommander(Attacker newOwner)
        {
            _owner = newOwner;
        }

        public override NodeState Evaluate()
        {
            float distance = (_owner.transform.position - _owner.Target.transform.position).magnitude;
            
            if (_owner.AttackRange > distance)
                return NodeState.Success;
            
            _owner.WalkToCommander();
            
            return NodeState.Working;
        }
    }
}
