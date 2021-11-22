using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public class MovingProjectile : DamageProjectile
    {
        protected float Speed { get; set; }
        
        public virtual void Init(Transform target, Caster caster, float damage, float speed)
        {
            Damage = damage;
            Speed = speed;
            Caster = caster;

            MoveProjectile(target);
        }

        protected void MoveProjectile(Transform target)
        {
            if (target == null) 
                return;
            var dir = (target.position - transform.position).normalized * Speed;
            myRigidbody.AddForce(dir, ForceMode.VelocityChange);
        }
    }
}