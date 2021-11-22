using Abilities.Ability.Scripts;
using Abilities.Effects.BuffAllUnitsInRangeCooldownEffect.Scripts;
using Abilities.Effects.IncreaseAttackDamageForXUnitsInXRangeEffect.Scripts;
using Abilities.VisitorPattern.Scripts;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.EffectNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/EffectNodes/BuffAllUnitsInRange",
        fileName = "BuffAllUnitsInRange")]
    public class BuffAllUnitsInRange : BaseNodes.Scripts.EffectNodes
    {
        [SerializeField] private float range;
        [SerializeField] private GameObject prefab;

        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is Ability ability))
                return;

            var owner = ability.Owner.GetComponent<Commander>();
            if (owner == null)
                return;
            var effect = ability.Owner.GetComponentInChildren<BuffAllUnitsInRangeCooldownEffect>();
            if (effect != null)
                return;
            var obj = Instantiate(prefab, ability.Owner.position, Quaternion.identity, ability.Owner);
            effect = obj.GetComponent<BuffAllUnitsInRangeCooldownEffect>();
            effect.Init(value, range);
        }
    }
}