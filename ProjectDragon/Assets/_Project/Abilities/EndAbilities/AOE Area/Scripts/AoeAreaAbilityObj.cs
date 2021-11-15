using Abilities.Ability.Scripts;
using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.Projectiles.Scripts;
using UnityEngine;

namespace Abilities.EndAbilities.AOE_Area.Scripts
{
    public class AoeAreaAbilityObj : DamageAbilityObj
    {
        [SerializeField] private float aoeRange;
        public float AoeRange => aoeRange;
        
        public void Cast(Transform spawnPoint, Caster caster, float abilityDamage, float abilityAoeRange)
        {
            //Spawn projectile
            var projectile = Instantiate(PrefabProjectile, spawnPoint.position, Quaternion.identity, spawnPoint).GetComponent<AoeProjectile>();
            projectile.Init(caster, abilityDamage, abilityAoeRange);
        }
        
        public AoeAreaAbility CreateInstance()
        {
            return new AoeAreaAbility(this);
        }
    }
    
    public class AoeAreaAbility : DamageAbility
    {
        public float AoeRange { get; set; }
        public AoeAreaAbility(AoeAreaAbilityObj abilityObj) : base(abilityObj)
        {
            AoeRange = abilityObj.AoeRange;
        }
        
        public override void Cast(Transform spawnPoint, Transform target, Caster caster)
        {
            if (TimeLeft > 0) 
                return;
            Casted = true;

            var aoeAreaAbility = (AoeAreaAbilityObj) AbilityObj;
            aoeAreaAbility.Cast(spawnPoint, caster, Damage, AoeRange);
        }
    }
}
