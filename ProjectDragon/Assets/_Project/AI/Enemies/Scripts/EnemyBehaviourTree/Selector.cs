using System.Collections.Generic;

namespace _Project.Enemies.Scripts.EnemyBehaviourTree
{
    public class Selector : Node
    {
        private List<Node> _nodes;

        private int _currentActiveChild;

        public Selector(List<Node> nodes)
        {
            this._nodes = nodes;
        }

        public override NodeState Evaluate()
        {
            NodeState result = _nodes[_currentActiveChild].Evaluate();

            switch (result)
            {
                // Returns Success after the current Node was successful.
                case NodeState.Success:
                    return NodeState.Success;
                // Goes to the next Node after one failure. 
                case NodeState.Failure:
                    _currentActiveChild++;
                    break;
            }

            // After reaching the last node with only failures returns a failure.
            if (_currentActiveChild >= _nodes.Count)
            {
                Reset();
                return NodeState.Failure;
            }
            else if (result == NodeState.Failure)
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
            foreach (var node  in _nodes)
            {
                node.Reset();
            }
        }
    }
}

