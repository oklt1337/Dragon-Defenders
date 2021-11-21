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
            Vector3 dir;
            if (target != null)
            {
                dir = (target.position - transform.position).normalized * Speed;
            }
            else
            {
                dir = (Vector3.forward - transform.position).normalized * Speed;
            }
            myRigidbody.AddForce(dir, ForceMode.VelocityChange);
        }
    }
}