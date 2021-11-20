using Abilities.EndAbilities.MeleeAttack.Scripts;
using Abilities.Projectiles.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/IncreaseMeleeAttackRange", fileName = "IncreaseMeleeAttackRange")]
    public class IncreaseMeleeAttackRange : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is MeleeAttackAbility ability)) 
                return;
            ability.AttackRange *= multiplier;
            var projectile = ability.Owner.GetComponentInChildren<MeleeProjectile>();
            var collider = projectile.GetComponent<BoxCollider>();
            var center = collider.center;
            center = new Vector3(center.z, center.y * multiplier, center.z);
            collider.center = center;
            var size = collider.size;
            size = new Vector3(size.x, size.y * multiplier, size.z);
            collider.size = size;

            var unit = ability.Owner.GetComponent<Unit>();
            if (unit != null)
                unit.SphereCollider.radius = ability.AttackRange;
        }
    }
}