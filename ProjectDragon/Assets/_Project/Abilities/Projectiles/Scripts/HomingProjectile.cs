using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public class HomingProjectile : MovingProjectile
    {
        private Transform Target { get; set; }
        
        public override void Init(Transform target, Caster caster, float damage, float speed)
        {
            Damage = damage;
            Speed = speed;
            Caster = caster;
            Target = target;
        }

        protected override void Update()
        {
            base.Update();
            MoveProjectile(Target);
        }
    }
}