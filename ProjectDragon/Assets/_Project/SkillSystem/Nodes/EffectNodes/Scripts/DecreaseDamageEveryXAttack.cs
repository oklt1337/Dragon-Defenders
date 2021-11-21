using Abilities.Ability.Scripts;
using Abilities.Effects.DecreaseDamageEveryXAttackEffect.Scripts;
using Abilities.VisitorPattern.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.EffectNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/EffectNodes/DecreaseDamageEveryXAttack",
        fileName = "DecreaseDamageEveryXAttack")]
    public class DecreaseDamageEveryXAttack : BaseNodes.Scripts.EffectNodes
    {
        [SerializeField] private int xTime;
        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is DamageAbility ability))
                return;
            var effect = ability.Owner.gameObject.AddComponent<DecreaseDamageEveryXAttackEffect>();
            effect.Init(value, xTime, ability);
        }
    }
}