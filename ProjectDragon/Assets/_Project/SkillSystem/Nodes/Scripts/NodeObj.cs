using Abilities.VisitorPattern.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.Scripts
{
    public enum NodeState
    {
        Deactivated,
        Learnable,
        Activated
    }
    public abstract class NodeObj : ScriptableObject
    {
        [SerializeField] private string nodeName;
        [SerializeField] private Sprite icon;
        [SerializeField] private int cost;
        
        public string NodeName => nodeName;
        public Sprite Icon => icon;
        public int Cost => cost;

        public abstract void Execute(IVisitor visitor);

        public Node CreateInstance(SkillTree.Scripts.SkillTree skillTree)
        {
            return new Node(this, skillTree);
        }
    }

    public sealed class Node : IElement
    {
        public NodeState NodeState { get; private set; }
        public NodeObj NodeObj { get; }

        private readonly SkillTree.Scripts.SkillTree skillTree;

        public Node(NodeObj nodeObj, SkillTree.Scripts.SkillTree newSkillTree)
        {
            NodeObj = nodeObj;
            skillTree = newSkillTree;
        }
        
        public void SetState(NodeState state)
        {
            NodeState = state;

            if (NodeState != NodeState.Activated) 
                return;

            foreach (var visitor in skillTree.Client.Visitors)
            {
                visitor.Visit(this);
            }

            foreach (var visitor in GlobalClients.Instance.Visitors)
            {
                visitor.Visit(this);
            }
        }

        public void Accept(IVisitor visitor)
        {
            NodeObj.Execute(visitor);
        }
    }
}
