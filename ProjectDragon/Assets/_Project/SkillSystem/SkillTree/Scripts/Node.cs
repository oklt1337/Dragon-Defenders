using UnityEngine;

namespace _Project.SkillSystem.SkillTree.Scripts
{
    public enum NodeState
    {
        Deactivated,
        Learnable,
        Activated
    }
    public class Node : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private int cost;
         
        public NodeState NodeState { get; private set; }
        public Sprite Icon => icon;
        public int Cost => cost;

        public virtual void EnableNode()
        {
            NodeState = NodeState.Activated;
        }
    }
}
