using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using UnityEngine;

namespace SkillSystem.SkillTree.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Nodes/EffectDamageAbility", fileName = "EffectDamageAbility")]
    public class EffectDamageAbilityRangeObj : NodeObj
    {
        [SerializeField] private float attackRangeMultiplier;
        
        public override void Execute(IVisitor visitor)
        {
            if (visitor is DamageAbility ability)
            {
                ability.AttackRange *= attackRangeMultiplier;
            }
        }
    }
}