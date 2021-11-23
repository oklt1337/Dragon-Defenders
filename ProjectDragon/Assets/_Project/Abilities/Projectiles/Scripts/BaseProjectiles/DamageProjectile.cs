using System;
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
        private const float MAXLifetime = 10;
        private float lifeTime;

        private void Awake()
        {
            lifeTime = MAXLifetime;
        }

        protected virtual void Update()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            Debug.Log("Destroy");
            Debug.Log(Caster);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            switch (Caster)
            {
                case Caster.Unit:
                    if (other.CompareTag("Enemy"))
                    {
                        Debug.Log($"Caster = {Caster} " + "Hit Enemy");
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
                        Debug.Log($"Caster = {Caster} " + "Hit Player");
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