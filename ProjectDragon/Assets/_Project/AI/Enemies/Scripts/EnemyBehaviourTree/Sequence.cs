using System.Collections.Generic;

namespace _Project.AI.Enemies.Scripts.EnemyBehaviourTree
{
    public class Sequence : Node
    {
        private readonly List<Node> _nodes;

        private int _currentActiveChild = 0;

        public Sequence(List<Node> nodes)
        {
            this._nodes = nodes;
        }


        public override NodeState Evaluate()
        {
            NodeState result = _nodes[_currentActiveChild].Evaluate();

            switch (result)
            {
                // Goes to the next node after succeeding.
                case NodeState.Success:
                    _currentActiveChild++;
                    break;
                // Stops after a single failure.
                case NodeState.Failure:
                    return result;
            }

            // After reaching the last node with only successes returns a success.
            if (_currentActiveChild >= _nodes.Count)
            {
                Reset();
                return NodeState.Success;
            }
            else if (result == NodeState.Success)
            {
                return Evaluate();
            }

            return NodeState.Working;
        }

        /// <summary>
        /// Resets current position and makes all its nodes reset themself.
        /// </summary>
        public override void Reset()
        {
            _currentActiveChild = 0;
            foreach (var node in _nodes)
            {
                node.Reset();
            }
        }
    }
}