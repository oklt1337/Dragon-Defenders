using Abilities.EndAbilities.IncreaseDamageForSetTime.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/IncreaseDamageForSetTimeAbilityDuration", fileName = "IncreaseDamageForSetTimeAbilityDuration")]
    public class IncreaseDamageForSetTimeAbilityDuration : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is IncreaseDamageForSetTimeAbility ability)
            {
                ability.Duration *= value;
            }
        }
    }
}