using System;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using AI.Enemies.Base_Enemy.Scripts;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public class MeleeProjectile : DamageProjectile
    {
        private float stunTime;
        public void Init(Caster caster, float damage, float stun)
        {
            Damage = damage;
            Caster = caster;
            stunTime = stun;
        }

        protected override void Update()
        {
        }

        protected override void OnTriggerEnter(Collider other)
        {
            switch (Caster)
            {
                case Caster.Unit:
                    if (other.CompareTag("Enemy"))
                    {
                        var enemy = other.GetComponent<Enemy>();
                        enemy.TakeDamage(Damage);
                        if (stunTime > 0)
                            enemy.Stun(stunTime);
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
    }
}
