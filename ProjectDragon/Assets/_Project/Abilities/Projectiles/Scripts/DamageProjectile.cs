using System;
using AI.Enemies.Base_Enemy;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public enum Caster
    {
        Unit,
        Commander,
        Enemy
    }
    public class DamageProjectile : Projectile
    {
        private float Damage { get; set; }
        private float Speed { get; set; }
        private Caster Caster { get; set; }

        public void Init(Transform target, Caster caster, float damage, float speed)
        {
            Damage = damage;
            Speed = speed;
            Caster = caster;
            
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
            switch (Caster)
            {
                case Caster.Unit:
                    if (other.CompareTag("Enemy"))
                    {
                        var enemy = other.GetComponent<Enemy>();
                        enemy.TakeDamage(Damage);
                        Destroy(gameObject);
                    }
                    break;
                case Caster.Commander:
                    if (other.CompareTag("Enemy"))
                    {
                        var enemy = other.GetComponent<Enemy>();
                        //enemy.TakeDamage(Damage);
                        Destroy(gameObject);
                    }
                    else if (other.CompareTag("Breakable"))
                    {
                        /*var baseMapObject = other.GetComponent<Tree>();
                        baseMapObject.TakeDamage(Damage);*/
                        Destroy(gameObject);
                    }
                    break;
                case Caster.Enemy:
                    if(other.CompareTag("Player"))
                    {
                        var commander = other.GetComponent<Commander>();
                        commander.TakeDamage(Damage);
                        Destroy(gameObject);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (other.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Enemy>();
                //enemy.TakeDamage(Damage);
                Destroy(gameObject);
            }
            else if(other.CompareTag("Player"))
            {
                var commander = other.GetComponent<Commander>();
                commander.TakeDamage(Damage);
                Destroy(gameObject);
            }
            else if (other.CompareTag("Breakable"))
            {
                /*var baseMapObject = other.GetComponent<Tree>();
                baseMapObject.TakeDamage(Damage);*/
                Destroy(gameObject);
            }
        }
    }
}