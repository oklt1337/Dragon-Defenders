using Abilities.Effects.IncreaseDamageEffect.Scripts;
using Abilities.EndAbilities.IncreaseDamageForSetTime.Scripts;
using Abilities.VisitorPattern.Scripts;
using Faction;
using SkillSystem.Nodes.BaseNodes.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/BuffValueForType", fileName = "BuffValueForType")]
    public class BuffValueForType : StatNode
    {
        [SerializeField] private ClassAndFaction.Class @class;
        
        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is IncreaseDamageForSetTimeAbility ability)) 
                return;
            ability.Casted += (t) =>
            {
                var unit = t.GetComponent<Unit>();
                if (unit == null) 
                    return;
                if (unit.UnitClass != @class)
                    return;
                var effect = unit.GetComponent<IncreaseDamageEffect>();
                if (effect == null) 
                    return;
                Destroy(effect);
                effect = unit.gameObject.AddComponent<IncreaseDamageEffect>();
                var value = ((IncreaseDamageForSetTimeAbilityObj) ability.AbilityAbilityObj)
                    .IncreaseAttackValueInPercentage * multiplier;
                effect.Init(ability.Duration, value);
            };
        }
    }
}