using Abilities.Ability.Scripts;
using Abilities.Projectiles.Scripts;
using UnityEngine;

namespace Abilities.EndAbilities.SingleShot.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Abilities/SingleShot", fileName = "SingleShot")]
    public class SingleShotAbilityObj : DamageAbilityObj
    {
        public void Cast(Transform spawnPoint, Transform target, float abilityDamage, float abilityProjectileSpeed)
        {
            //Spawn projectile
            var damageProjectile = Instantiate(PrefabProjectile, spawnPoint.position, Quaternion.identity, spawnPoint).GetComponent<DamageProjectile>();
            damageProjectile.Init(target != null ? target : null, abilityDamage, abilityProjectileSpeed);
        }
        
        public SingleShotAbility CreateInstance()
        {
            return new SingleShotAbility(this);
        }
    }
    
    public class SingleShotAbility : DamageAbility
    {
        public SingleShotAbility(DamageAbilityObj abilityObj) : base(abilityObj)
        {
        }
        
        public override void Cast(Transform spawnPoint, Transform target)
        {
            if (TimeLeft > 0) 
                return;
            Casted = true;

            var singeShotAbilityObj = (SingleShotAbilityObj) AbilityObj;
            singeShotAbilityObj.Cast(spawnPoint, target, Damage, ProjectileSpeed);
        }
    }
}