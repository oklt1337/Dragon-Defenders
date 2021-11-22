using Abilities.Effects.ReduceUnitCooldownInRangeEffect.Scripts;
using Abilities.VisitorPattern.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace SkillSystem.Nodes.EffectNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/EffectNodes/ReduceUnitCooldownInRange", fileName = "ReduceUnitCooldownInRange")]
    public class ReduceUnitCooldownInRange : BaseNodes.Scripts.EffectNodes
    {
        [SerializeField] private float range;
        [SerializeField] private GameObject prefab;
        
        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is Unit unit)) 
                return;
            
            var effect = unit.GetComponentInChildren<ReduceUnitCooldownInRangeEffect>();
            if (effect != null)
                return;
            var transform = unit.transform;
            var obj = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            effect = obj.GetComponent<ReduceUnitCooldownInRangeEffect>();
            effect.Init(value, range, unit);
        }
    }
}