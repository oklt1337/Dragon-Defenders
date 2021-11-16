﻿using System;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using AI.Enemies.Base_Enemy;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public class AoeProjectile : DamageProjectile
    {
        private float Duration { get; set; }
        
        public void Init(Caster caster, float damage, float aoeRange, float duration)
        {
            Damage = damage;
            Caster = caster;
            Duration = duration;
            ((SphereCollider) myCollider).radius = aoeRange;
        }

        private void Update()
        {
            Duration -= Time.deltaTime;
            if (Duration <= 0)
            {
                Destroy(gameObject);
            }
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