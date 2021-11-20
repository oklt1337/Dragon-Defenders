using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/IncreaseDamage", fileName = "IncreaseDamage")]
    public class IncreaseDamage : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is DamageAbility ability)
            {
                ability.Damage *= multiplier;
            }
        }
    }
}