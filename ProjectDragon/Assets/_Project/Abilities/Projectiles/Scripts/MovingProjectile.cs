using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public class MovingProjectile : DamageProjectile
    {
        protected float Speed { get; set; }
        protected Vector3 direction;
        
        public virtual void Init(Transform target, Caster caster, float damage, float speed)
        {
            Damage = damage;
            Speed = speed;
            Caster = caster;
            direction = (target.position - transform.position).normalized * Speed;
        }
        
        public void Init(Vector3 dir, Caster caster, float damage, float speed)
        {
            Damage = damage;
            Speed = speed;
            Caster = caster;
            direction = dir * Speed;
        }

        protected override void Update()
        {
            base.Update();
            MoveProjectile();
        }

        protected void MoveProjectile()
        {
            var myTransform = transform;
            var position = myTransform.position;
            position += direction * Time.deltaTime;
            myTransform.position = position;
        }
    }
}