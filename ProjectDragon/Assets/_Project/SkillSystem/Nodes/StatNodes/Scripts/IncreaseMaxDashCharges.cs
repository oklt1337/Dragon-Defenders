using Abilities.EndAbilities.DashInLookDirection.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/IncreaseMaxDashCharges", fileName = "IncreaseMaxDashCharges")]
    public class IncreaseMaxDashCharges : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is DashInLookDirectionAbility ability)
            {
                ability.MaxCharges += (int) value;
            }
        }
    }
}