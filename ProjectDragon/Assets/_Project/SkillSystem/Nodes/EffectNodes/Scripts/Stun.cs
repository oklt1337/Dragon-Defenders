using Abilities.Ability.Scripts;
using Abilities.EndAbilities.MeleeAttack.Scripts;
using Abilities.VisitorPattern.Scripts;
using Unity.Burst;
using UnityEngine;

namespace SkillSystem.Nodes.EffectNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/EffectNodes/KnockBack", fileName = "KnockBack")]
    public class Stun : BaseNodes.Scripts.EffectNodes
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is MeleeAttackAbility ability)
            {
                ability.StunTime = value;
            }
        }
    }
}