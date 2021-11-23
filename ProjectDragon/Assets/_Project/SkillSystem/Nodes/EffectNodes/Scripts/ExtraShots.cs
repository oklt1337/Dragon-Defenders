using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using Abilities.VisitorPattern.Scripts;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace SkillSystem.Nodes.EffectNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/EffectNodes/ExtraShots", fileName = "ExtraShots")]
    public class ExtraShots : BaseNodes.Scripts.EffectNodes
    {
        private Unit unit;
        private SingleShotAbility singleShotAbility;
        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is SingleShotAbility ability)) 
                return; 
            
            unit = ability.Owner.GetComponent<Unit>();
            singleShotAbility = ability.AbilityAbilityObj.CreateInstance<SingleShotAbility>();
            
            ability.Casted += Shot;
        }

        private void Shot(Transform target)
        {
            var dir = target.position - unit.SpawnPos.position;
            var up = unit.SpawnPos.up;
            var point = Quaternion.AngleAxis(value, up) * dir;
            var point2 = Quaternion.AngleAxis(-value, up) * dir;
            
            singleShotAbility.Cast(unit.SpawnPos, point.normalized, Caster.Unit);
            singleShotAbility.Cast(unit.SpawnPos, point2.normalized, Caster.Unit);
        }
    }
}