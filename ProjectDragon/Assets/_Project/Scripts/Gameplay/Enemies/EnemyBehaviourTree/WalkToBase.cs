using System;

namespace _Project.Scripts.Gameplay.Enemies.EnemyBehaviourTree
{
    public class WalkToBase : Node
    {
        private PopcornEnemy _owner;

        public WalkToBase(PopcornEnemy owner)
        {
            _owner = owner;
        }

        public override NodeState Evaluate()
        {
            _owner.WalkToBase();
            return NodeState.Working;
        }
    }
}
