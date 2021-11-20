using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.EndAbilities.SingleShotSetRange.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/Abilities/DamageAbilities/SingleShotSetRange", fileName = "SingleShotSetRange")]
    public class SingleShotSetRangeAbilityObj : SingleShotAbilityObj
    {
        [SerializeField] private float travelRange;
        public float TravelRange => travelRange;
        
        public void Cast(Transform spawnPoint, Transform target, Caster caster, float abilityDamage , float abilityProjectileSpeed, float abilityTravelRange)
        {
            //Spawn projectile
            var projectile = Instantiate(PrefabProjectile, spawnPoint.position, Quaternion.identity).GetComponent<MovingSetRangeProjectile>();
            projectile.Init(target, caster, abilityDamage, abilityProjectileSpeed, abilityProjectileSpeed);
            FixRotation(target, projectile.transform);
        }

        public override T CreateInstance<T>()
        {
            return new SingleShotSetRangeAbility(this) as T;
        }
    }
    public class SingleShotSetRangeAbility : SingleShotAbility
    {
        public float TravelRange { get; set; }
        
        public SingleShotSetRangeAbility(SingleShotSetRangeAbilityObj abilityAbilityObj) : base(abilityAbilityObj)
        {
            TravelRange = abilityAbilityObj.TravelRange;
        }
        
        public override void Cast(Transform spawnPoint, Transform target, Caster caster)
        {
            if (TimeLeft > 0) 
                return;
            StartCooldown = true;
            Casted?.Invoke(target);
            
            var abilityObj = (SingleShotSetRangeAbilityObj) AbilityAbilityObj;
            abilityObj.Cast(spawnPoint, target, caster, Damage, ProjectileSpeed, TravelRange);
        }
    }
}
