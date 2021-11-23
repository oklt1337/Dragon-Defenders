using Abilities.Effects.IncreaseDamageEffect.Scripts;
using Abilities.EndAbilities.IncreaseDamageForSetTime.Scripts;
using Abilities.VisitorPattern.Scripts;
using Faction;
using SkillSystem.Nodes.BaseNodes.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/BuffValueForType",
        fileName = "BuffValueForType")]
    public class BuffValueForType : StatNode
    {
        [SerializeField] private ClassAndFaction.Class @class;

        private IncreaseDamageForSetTimeAbility increaseDamageForSetTimeAbility;
        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is IncreaseDamageForSetTimeAbility ability))
                return;

            increaseDamageForSetTimeAbility = ability.AbilityAbilityObj.CreateInstance<IncreaseDamageForSetTimeAbility>();
            ability.Casted += ApplyBuff;
            if (ability.CurrenTarget == null)
                return;
            ApplyBuff(increaseDamageForSetTimeAbility.CurrenTarget);
        }

        private void ApplyBuff(Transform target)
        {
            var unit = target.GetComponent<Unit>();
            if (unit == null)
                return;
            if (unit.UnitClass != @class)
                return;
            var effect = unit.gameObject.GetComponent<IncreaseDamageEffect>();
            if (effect != null)
            {
                effect.OnEffectDestroyed += () => AddEffect(unit);
                Destroy(effect);
            }
            else
            {
                AddEffect(unit);
            }
        }

        private void AddEffect(Component unit)
        {
            var effect = unit.gameObject.AddComponent<IncreaseDamageEffect>();
            effect.Init(increaseDamageForSetTimeAbility.Duration, value);
        }
    }
}