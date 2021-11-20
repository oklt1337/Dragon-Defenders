using Faction;
using UnityEngine;

namespace SkillSystem.Nodes.BaseNodes.Scripts
{
    public abstract class ClassNodes : NodeObj
    {
        [SerializeField] protected ClassAndFaction.Class @class;
    }
}