using System.Collections.Generic;
using UnityEngine;

namespace _Project.SkillSystem.SkillTree.Scripts
{
    public class SkillTree : ScriptableObject
    {
        public readonly Dictionary<int, Node> Nodes = new Dictionary<int, Node>();

        private int currentRow;

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