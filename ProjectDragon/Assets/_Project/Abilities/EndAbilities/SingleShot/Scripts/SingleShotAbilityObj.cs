using Abilities.Ability.Scripts;
using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.EndAbilities.SingleShot.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/Abilities/DamageAbilities/SingleShot", fileName = "SingleShot")]
    public class SingleShotAbilityObj : DamageAbilityObj
    {
        [SerializeField] private float projectileSpeed;
        public float ProjectileSpeed => projectileSpeed;
        
        public virtual void Cast(Transform spawnPoint, Transform target, Caster caster, float abilityDamage , float abilityProjectileSpeed)
        {
            //Spawn projectile
            var projectile = Instantiate(PrefabProjectile, spawnPoint.position, Quaternion.identity).GetComponent<MovingProjectile>();
            projectile.Init(target, caster, abilityDamage, abilityProjectileSpeed);
            FixRotation(target, projectile.transform);
        }
        
        protected static void FixRotation(Transform target, Transform projectile)
        {
            if (target == null) 
                return;
            var rotation = Quaternion.LookRotation(target.position - projectile.position, Vector3.up).eulerAngles;
            rotation.z = 0;
            rotation.x = 0;
            projectile.rotation = Quaternion.Euler(rotation);
        }

        public override T CreateInstance<T>()
        {
            return new SingleShotAbility(this) as T;
        }
    }
    
    public class SingleShotAbility : DamageAbility
    {
        public float ProjectileSpeed { get; set; }
        
        public SingleShotAbility(SingleShotAbilityObj abilityAbilityObj) : base(abilityAbilityObj)
        {
            ProjectileSpeed = abilityAbilityObj.ProjectileSpeed;
        }
        
        public override void Cast(Transform spawnPoint, Transform target, Caster caster)
        {
            if (TimeLeft > 0) 
                return;
            StartCooldown = true;
            Casted?.Invoke(target);
            
            var singeShotAbilityObj = (SingleShotAbilityObj) AbilityAbilityObj;
            singeShotAbilityObj.Cast(spawnPoint, target, caster, Damage, ProjectileSpeed);
        }
    }
}