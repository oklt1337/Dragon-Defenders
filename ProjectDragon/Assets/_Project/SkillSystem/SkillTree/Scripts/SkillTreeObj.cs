using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem.SkillTree.Scripts
{
    public class SkillTreeObj : ScriptableObject
    {
        public readonly Dictionary<int, NodeObj> NodeObjs = new Dictionary<int, NodeObj>();
        
        public SkillTree CreateInstance()
        {
            return new SkillTree(this);
        }
    }

    public class SkillTree
    {
        public readonly Dictionary<int, Node> Nodes = new Dictionary<int, Node>();
        private int currentRow;

        public SkillTree(SkillTreeObj skillTreeObj)
        {
            foreach (var node in skillTreeObj.NodeObjs)
            {
                Nodes.Add(node.Key ,node.Value.CreateInstance());
            }
        }

        public void SetNodeActive(int nodeIndex)
        {
            Nodes[nodeIndex].SetState(NodeState.Activated);

            // Deactivate other Nodes
            if (Nodes[nodeIndex + 1].NodeState == NodeState.Learnable)
            {
                Nodes[nodeIndex + 1].SetState(NodeState.Deactivated);
            }
            else if (Nodes[nodeIndex - 1].NodeState == NodeState.Learnable)
            {
                Nodes[nodeIndex - 1].SetState(NodeState.Deactivated);
            }

            // Set new nodes Learnable
            var index = currentRow + nodeIndex;
            for (var i = 1; i < 3; i++)
            {
                Nodes[index + i].SetState(NodeState.Learnable);
            }
            currentRow++;
        }
    }
}