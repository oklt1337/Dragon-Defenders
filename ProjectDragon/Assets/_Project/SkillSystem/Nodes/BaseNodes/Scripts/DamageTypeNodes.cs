using Abilities.Ability.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.BaseNodes.Scripts
{
    public abstract class DamageTypeNodes : NodeObj
    {
        [SerializeField] protected DamageType damageType;
    }
}