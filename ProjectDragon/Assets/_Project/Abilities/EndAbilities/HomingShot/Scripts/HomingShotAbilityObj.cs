using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.EndAbilities.HomingShot.Scripts
{
    public class HomingShotAbilityObj : SingleShotAbilityObj
    {
        public override void Cast(Transform spawnPoint, Transform target, Caster caster, float abilityDamage , float abilityProjectileSpeed)
        {
            //Spawn projectile
            var projectile = Instantiate(PrefabProjectile, spawnPoint.position, Quaternion.identity, spawnPoint).GetComponent<MovingProjectile>();
            projectile.Init(target, caster, abilityDamage, abilityProjectileSpeed);
        }
        
        public new HomingShotAbility CreateInstance()
        {
            return new HomingShotAbility(this);
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