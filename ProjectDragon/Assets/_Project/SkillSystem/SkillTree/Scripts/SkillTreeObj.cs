using System.Collections.Generic;
using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace SkillSystem.SkillTree.Scripts
{
    public class SkillTreeObj : ScriptableObject
    {
        public readonly Dictionary<int, NodeObj> NodeObjs = new Dictionary<int, NodeObj>();
        
        public SkillTree CreateInstance(Client client)
        {
            return new SkillTree(this, client);
        }
    }

    public class SkillTree
    {
        public readonly Dictionary<int, Node> Nodes = new Dictionary<int, Node>();
        private int currentRow;
        public Client Client { get; }

        public SkillTree(SkillTreeObj skillTreeObj,Client client)
        {
            Client = client;
            foreach (var node in skillTreeObj.NodeObjs)
            {
                Nodes.Add(node.Key ,node.Value.CreateInstance(this));
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