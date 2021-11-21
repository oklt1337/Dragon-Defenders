using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/ReduceCooldown", fileName = "ReduceCooldown")]
    public class ReduceCooldown : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is Ability ability)
            {
                ability.CoolDown *= value;
            }
        }
    }
}