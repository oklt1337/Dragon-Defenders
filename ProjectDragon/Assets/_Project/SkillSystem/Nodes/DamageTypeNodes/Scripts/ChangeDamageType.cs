using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.DamageTypeNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/DamageTypeNodes/ChangeDamageType", fileName = "ChangeDamageType")]
    public class ChangeDamageType : BaseNodes.Scripts.DamageTypeNodes
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is DamageAbility ability)
            {
                ability.DamageType = damageType;
            }
        }
    }
}