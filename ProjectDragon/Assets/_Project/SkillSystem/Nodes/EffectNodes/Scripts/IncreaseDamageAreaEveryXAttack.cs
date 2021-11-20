using Abilities.Effects.IncreaseDamageAreaEveryXAttackEffect.Scripts;
using Abilities.EndAbilities.MeleeAttack.Scripts;
using Abilities.VisitorPattern.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.EffectNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/EffectNodes/IncreaseDamageAreaEveryXAttack",
        fileName = "IncreaseDamageAreaEveryXAttack")]
    public class IncreaseDamageAreaEveryXAttack : BaseNodes.Scripts.EffectNodes
    {
        [SerializeField] private int xTime;
        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is MeleeAttackAbility ability)) 
                return;
            var effect = ability.Owner.gameObject.AddComponent<IncreaseDamageAreaEveryXAttackEffect>();
            effect.Init(value, xTime, ability);
        }
    }
}