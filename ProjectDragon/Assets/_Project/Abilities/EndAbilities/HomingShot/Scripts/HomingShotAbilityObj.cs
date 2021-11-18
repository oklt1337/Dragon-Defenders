using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.EndAbilities.HomingShot.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Abilities/HomingShot", fileName = "HomingShot")]
    public class HomingShotAbilityObj : SingleShotAbilityObj
    {
        public override void Cast(Transform spawnPoint, Transform target, Caster caster, float abilityDamage , float abilityProjectileSpeed)
        {
            //Spawn projectile
            var projectile = Instantiate(PrefabProjectile, spawnPoint.position, Quaternion.identity, spawnPoint).GetComponent<MovingProjectile>();
            projectile.Init(target, caster, abilityDamage, abilityProjectileSpeed);
        }
        
        public override T CreateInstance<T>()
        {
            return new HomingShotAbility(this) as T;
        }
    }

    public class HomingShotAbility : SingleShotAbility
    {
        public HomingShotAbility(SingleShotAbilityObj abilityObj) : base(abilityObj)
        {
        }

        public override void Cast(Transform spawnPoint, Transform target, Caster caster)
        {
            if (TimeLeft > 0) 
                return;
            StartCooldown = true;
            Casted?.Invoke();
            
            var singeShotAbilityObj = (HomingShotAbilityObj) AbilityObj;
            singeShotAbilityObj.Cast(spawnPoint, target, caster, Damage, ProjectileSpeed);
        }
    }
}