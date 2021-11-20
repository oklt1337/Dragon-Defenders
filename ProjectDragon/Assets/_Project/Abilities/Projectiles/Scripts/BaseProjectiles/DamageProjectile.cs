﻿using System;
using AI.Enemies.Base_Enemy;
using AI.Enemies.Base_Enemy.Scripts;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace Abilities.Projectiles.Scripts.BaseProjectiles
{
    public enum Caster
    {
        Unit,
        Commander,
        Enemy
    }
    public abstract class DamageProjectile : Projectile
    {
        protected float Damage { get; set; }
        protected Caster Caster { get; set; }
        private float maxLifetime = 10;
        protected virtual void Update()
        {
            maxLifetime -= Time.deltaTime;
            if (maxLifetime < 0)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.tag);
            switch (Caster)
            {
                case Caster.Unit:
                    if (other.CompareTag("Enemy"))
                    {
                        Debug.Log("Enemy");
                        var enemy = other.GetComponent<Enemy>();
                        enemy.TakeDamage(Damage);
                        Destroy(gameObject);
                    }
                    break;
                case Caster.Commander:
                    if (other.CompareTag("Enemy"))
                    {
                        var enemy = other.GetComponent<Enemy>();
                        enemy.TakeDamage(Damage);
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
        }
    }
}