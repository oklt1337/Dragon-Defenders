using System;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using AI.Enemies.Base_Enemy.Scripts;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public sealed class MovingSetRangeProjectile : MovingProjectile
    {
        private float TravelRange { get; set; }
        private Vector3 startPos;

        protected override void OnTriggerEnter(Collider other)
        {
            switch (Caster)
            {
                case Caster.Unit:
                    if (other.CompareTag("Enemy"))
                    {
                        var enemy = other.GetComponent<Enemy>();
                        enemy.TakeDamage(Damage);
                    }
                    break;
                case Caster.Commander:
                    if (other.CompareTag("Enemy"))
                    {
                        var enemy = other.GetComponent<Enemy>();
                        enemy.TakeDamage(Damage);
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
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void Update()
        {
            base.Update();
            Debug.Log(Vector3.Distance(startPos, transform.position));
            if (Math.Abs(Vector3.Distance(startPos, transform.position) - TravelRange) < 0.1f)
            {
                Destroy(gameObject);
            }
        }

        public void Init(Transform target, Caster caster, float damage, float speed, float range)
        {
            Damage = damage;
            Speed = speed;
            Caster = caster;
            TravelRange = range;
            startPos = transform.position;
            
            MoveProjectile(target);
        }
    }
}