using Abilities.Ability.Scripts;
using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.EndAbilities.SingleShot.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Abilities/SingleShot", fileName = "SingleShot")]
    public class SingleShotAbilityObj : DamageAbilityObj
    {
        [SerializeField] private float projectileSpeed;
        public float ProjectileSpeed => projectileSpeed;
        
        public virtual void Cast(Transform spawnPoint, Transform target, Caster caster, float abilityDamage , float abilityProjectileSpeed)
        {
            //Spawn projectile
            var projectile = Instantiate(PrefabProjectile, spawnPoint.position, Quaternion.identity, spawnPoint).GetComponent<MovingProjectile>();
            projectile.Init(target, caster, abilityDamage, abilityProjectileSpeed);
        }

        public override T CreateInstance<T>()
        {
            return new SingleShotAbility(this) as T;
        }
    }
    
    public class SingleShotAbility : DamageAbility
    {
        public float ProjectileSpeed { get; set; }
        
        public SingleShotAbility(SingleShotAbilityObj abilityObj) : base(abilityObj)
        {
            ProjectileSpeed = abilityObj.ProjectileSpeed;
        }
        
        public override void Cast(Transform spawnPoint, Transform target, Caster caster)
        {
            if (TimeLeft > 0) 
                return;
            StartCooldown = true;
            Casted?.Invoke();
            
            var singeShotAbilityObj = (SingleShotAbilityObj) AbilityObj;
            singeShotAbilityObj.Cast(spawnPoint, target, caster, Damage, ProjectileSpeed);
        }
    }
}