using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/IncreaseBuffRange", fileName = "IncreaseBuffRange")]
    public class IncreaseBuffRange : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is UtilityAbility ability)) 
                return;
            ability.EffectRange *= value;
            var unit = ability.Owner.GetComponent<Unit>();
            if (unit != null)
            {
                unit.SphereCollider.radius = ability.EffectRange;
            }
        }
    }
}