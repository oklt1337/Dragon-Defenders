using Abilities.EndAbilities.AOE_Area.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/IncreaseAoeRange", fileName = "IncreaseAoeRange")]
    public class IncreaseAoeRange : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is AoeAreaAbility ability))
                return;
            ability.AoeRange *= multiplier;
        }
    }
}