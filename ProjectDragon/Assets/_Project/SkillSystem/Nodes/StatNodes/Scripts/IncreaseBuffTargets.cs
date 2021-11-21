using Abilities.EndAbilities.DecreaseCooldownOfDamageAbility.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/IncreaseBuffTargets", fileName = "IncreaseBuffTargets")]
    public class IncreaseBuffTargets : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is DecreaseCooldownOfDamageAbility ability)
            {
                ability.MaxTargets = multiplier;
            }
        }
    }
}