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
        private SingleShotAbility singleShotAbility;
        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is SingleShotAbility ability)) 
                return;
            ability.Casted += Shot;
            singleShotAbility = ability;
        }

        private void Shot()
        {
            var unit = singleShotAbility.Owner.GetComponent<Unit>();
            var commander = singleShotAbility.Owner.GetComponent<Commander>();
            if (unit != null)
            {
                singleShotAbility.Cast(unit.SpawnPos, GetDirectionTransform(value), Caster.Unit);
                singleShotAbility.Cast(unit.SpawnPos, GetDirectionTransform(-value), Caster.Unit);
            }
            else if (commander != null)
            {
                singleShotAbility.Cast(commander.transform, GetDirectionTransform(value), Caster.Commander);
                singleShotAbility.Cast(commander.transform, GetDirectionTransform(-value), Caster.Commander);
            }
        }

        private Transform GetDirectionTransform(float eulerAngle)
        {
            var target = singleShotAbility.Owner.rotation;
            var euler = target.eulerAngles;
            euler.y = eulerAngle;
            Transform shotTrans = new RectTransform();
            shotTrans.rotation = Quaternion.Euler(euler);
            shotTrans.position = singleShotAbility.Owner.forward;

            return shotTrans;
        }
    }
}