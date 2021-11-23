using Abilities.Projectiles.Scripts.BaseProjectiles;
using AI.Enemies.Base_Enemy.Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public class HomingProjectile : MovingProjectile
    {
        private Transform Target { get; set; }
        private Enemy enemy;
        
        public override void Init(Transform target, Caster caster, float damage, float speed)
        {
            Damage = damage;
            Speed = speed;
            Caster = caster;
            Target = target;

            enemy = target.GetComponent<Enemy>();
            if (enemy != null)
                enemy.OnDeath += DestroyMe;
        }

        protected override void Update()
        {
            base.Update();
            MoveProjectile(Target);
        }

        private void MoveProjectile(Transform target)
        {
            if (target == null) 
                return;
            var myTransform = transform;
            var position = myTransform.position;
            var dir = (target.position - position).normalized * Speed;
            position += dir * Time.deltaTime;
            myTransform.position = position;
        }
        
        private void DestroyMe(bool b)
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (enemy != null)
                enemy.OnDeath -= DestroyMe;
        }
    }
}