using AI.Enemies.Base_Enemy;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public class DamageProjectile : Projectile
    {
        private float Damage { get; set; }
        private float Speed { get; set; }

        public void Init(Transform target, float damage, float speed)
        {
            Damage = damage;
            Speed = speed;

            MoveProjectile(target);
        }

        private void MoveProjectile(Transform target)
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
            myRigidbody.velocity = dir;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Enemy>();
                enemy.TakeDamage(Damage);
            }
            else if(other.CompareTag("Player"))
            {
                var commander = other.GetComponent<Commander>();
                commander.TakeDamage(Damage);
            }
            else if (other.CompareTag("Breakable"))
            {
                /*var baseMapObject = other.GetComponent<Tree>();
                baseMapObject.TakeDamage(Damage);*/
            }
            
            Destroy(gameObject);
        }
    }
}