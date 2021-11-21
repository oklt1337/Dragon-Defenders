using System.Collections.Generic;
using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using Sirenix.Serialization;
using SkillSystem.Nodes.BaseNodes.Scripts;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace SkillSystem.SkillTree.Scripts
{
    public class SkillTreeObj : ScriptableObject
    {
        [SerializeField] private List<NodeObj> nodeObjs = new List<NodeObj>();
        public List<NodeObj> NodeObjs => nodeObjs;

        public SkillTree CreateInstance(Client client)
        {
            return new SkillTree(this, client);
        }
    }

    public class SkillTree
    {
        public readonly List<Node> Nodes = new List<Node>();
        private int currentRow;
        public Client Client { get; }

        public SkillTree(SkillTreeObj skillTreeObj, Client client)
        {
            Client = client;
            foreach (var node in skillTreeObj.NodeObjs)
            {
                Nodes.Add(node.CreateInstance(this));
            }

            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].SetState(i < 2 ? NodeState.Learnable : NodeState.Deactivated);
            }
        }

        public void SetNodeActive(int nodeIndex)
        {
            if (Nodes[nodeIndex].NodeState == NodeState.Activated)
                return;

            Nodes[nodeIndex].SetState(NodeState.Activated);

            // Deactivate other Nodes
            if (nodeIndex + 1 < Nodes.Count)
            {
                if (Nodes[nodeIndex + 1].NodeState == NodeState.Learnable)
                {
                    Nodes[nodeIndex + 1].SetState(NodeState.Deactivated);
                }
            }

            if (nodeIndex - 1 >= 0)
            {
                if (Nodes[nodeIndex - 1].NodeState == NodeState.Learnable)
                {
                    Nodes[nodeIndex - 1].SetState(NodeState.Deactivated);
                }
            }

            // Set new nodes Learnable
            var index = currentRow + nodeIndex + 1;

            for (var i = 1; i < 3; i++)
            {
                if (index + i < Nodes.Count)
                {
                    Nodes[index + i].SetState(NodeState.Learnable);
                }
            }

            currentRow++;
        }
    }
}