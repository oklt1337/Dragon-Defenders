using System.Collections.Generic;
using UnityEngine;

namespace _Project.SkillSystem.SkillTree.Scripts
{
    public class SkillTree : ScriptableObject
    {
        public readonly Dictionary<int, Node> Nodes = new Dictionary<int, Node>();

        public void SetNodeActive(int nodeIndex)
        {
            Nodes[nodeIndex].EnableNode();
            
            //set other learnable
        }
    }
}
