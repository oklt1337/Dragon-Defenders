using UnityEngine;

namespace SkillSystem.SkillTree.Scripts
{
    public enum NodeState
    {
        Deactivated,
        Learnable,
        Activated
    }
    public class NodeObj : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private int cost;
        
        public Sprite Icon => icon;
        public int Cost => cost;

        public void Execute()
        {
            
        }

        public Node CreateInstance()
        {
            return new Node(this);
        }
    }

    public class Node
    {
        public NodeState NodeState { get; private set; }
        public NodeObj NodeObj { get; }

        public Node(NodeObj nodeObj)
        {
            NodeObj = nodeObj;
        }
        
        public virtual void SetState(NodeState state)
        {
            NodeState = state;

            if (NodeState == NodeState.Activated)
            {
                NodeObj.Execute();
            }
        }
    }
}
