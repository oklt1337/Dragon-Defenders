using Abilities.Effects.EveryXTimeIncreaseShotSpeedForXTimeEffect.Scripts;
using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.VisitorPattern.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.EffectNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/EffectNodes/EveryXTimeIncreaseShotSpeedForXTime", fileName = "EveryXTimeIncreaseShotSpeedForXTime")]
    public class EveryXTimeIncreaseShotSpeedForXTime : BaseNodes.Scripts.EffectNodes
    {
        [SerializeField] private float cooldown;
        [SerializeField] private float effectCooldown;

        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is SingleShotAbility ability))
                return;
            var effect = ability.Owner.gameObject.AddComponent<EveryXTimeIncreaseShotSpeedForXTimeEffect>();
            effect.Init(ability,cooldown, effectCooldown, value);
        }
    }
}