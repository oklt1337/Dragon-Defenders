using UnityEngine;

namespace _Project.Scripts.Gameplay.Enemies.EnemyBehaviourTree
{
    public class FlyToBase : Node
    {
        private Flier _owner;

        public FlyToBase(Flier newOwner)
        {
            _owner = newOwner;
        }

        public override NodeState Evaluate()
        {
            _owner.FlyToBase();
            return NodeState.Working;
        }
    }
}
