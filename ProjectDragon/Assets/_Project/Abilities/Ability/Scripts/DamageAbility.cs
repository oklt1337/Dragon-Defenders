using Abilities.Projectiles.Scripts;
using UnityEngine;

namespace Abilities.Ability.Scripts
{
    public abstract class DamageAbilityObj : AbilityObj
    {
        [SerializeField] private float damage;
        [SerializeField] private float attackRange;
        [SerializeField] private float projectileSpeed;
        
        [SerializeField] private GameObject prefabProjectile;

        public float Damage => damage;
        public float AttackRange => attackRange; 
        public float ProjectileSpeed => projectileSpeed;
        
        public DamageAbility CreateInstance()
        {
            return new DamageAbility(this);
        }

        public void Cast(Transform spawnPoint, Transform target, float abilityDamage, float abilityProjectileSpeed)
        {
            //Spawn projectile
            var damageProjectile = Instantiate(prefabProjectile, spawnPoint.position, Quaternion.identity, spawnPoint).GetComponent<DamageProjectile>();
            damageProjectile.Init(target != null ? target : null, abilityDamage, abilityProjectileSpeed);
        }
    }
    
    public class DamageAbility : Ability
    {
        public float Damage { get; set; }
        public float AttackRange { get; set; }
        public float ProjectileSpeed { get; set; }

        public DamageAbility(DamageAbilityObj abilityObj) : base(abilityObj)
        {
            Damage = abilityObj.Damage;
            AttackRange = abilityObj.AttackRange;
            ProjectileSpeed = abilityObj.ProjectileSpeed;
        }

        public void Cast(Transform spawnPoint, Transform target)
        {
            if (TimeLeft > 0) 
                return;
            
            var damageAbilityObj = (DamageAbilityObj) AbilityObj;
            damageAbilityObj.Cast(spawnPoint, target, Damage, ProjectileSpeed);
            Casted = true;
        }
    }
}
