using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public class AoeProjectile : DamageProjectile
    {
        public void Init(Caster caster, float damage, float aoeRange)
        {
            Damage = damage;
            Caster = caster;
            ((SphereCollider) myCollider).radius = aoeRange;
        }
    }
}