using Abilities.Ability.Scripts;
using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.EndAbilities.AOE_Area.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Abilities/AoeArea", fileName = "AoeArea")]
    public class AoeAreaAbilityObj : DamageAbilityObj
    {
        [SerializeField] private float aoeRange;
        [SerializeField] private float duration;
        public float AoeRange => aoeRange;
        public float Duration => duration;
        
        public void Cast(Transform spawnPoint, Caster caster, float abilityDamage, float abilityAoeRange, float abilityDuration)
        {
            //Spawn projectile
            var projectile = Instantiate(PrefabProjectile, spawnPoint.position, Quaternion.identity, spawnPoint).GetComponent<AoeProjectile>();
            projectile.Init(caster, abilityDamage, abilityAoeRange, abilityDuration);
        }

        public override T CreateInstance<T>()
        {
            return new AoeAreaAbility(this) as T;
        }
    }
    
    public class AoeAreaAbility : DamageAbility
    {
        public float AoeRange { get; set; }
        public float Duration { get; set; }
        public AoeAreaAbility(AoeAreaAbilityObj abilityObj) : base(abilityObj)
        {
            AoeRange = abilityObj.AoeRange;
            Duration = abilityObj.Duration;
        }
        
        public override void Cast(Transform spawnPoint, Transform target, Caster caster)
        {
            if (TimeLeft > 0) 
                return;
            StartCooldown = true;
            Casted?.Invoke();

            var aoeAreaAbility = (AoeAreaAbilityObj) AbilityAbilityObj;
            aoeAreaAbility.Cast(spawnPoint, caster, Damage, AoeRange, Duration);
        }
    }
}
