using Abilities.Ability.Scripts;
using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.EndAbilities.IncreaseDamageForSetTime.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Abilities/IncreaseDamageForSetTime", fileName = "IncreaseDamageForSetTime")]
    public class IncreaseDamageForSetTimeAbilityObj : UtilityAbilityObj
    {
        [SerializeField] private float increaseAttackValueInPercentage;
        public float IncreaseAttackValueInPercentage => increaseAttackValueInPercentage;
        
        public void Cast(Transform spawnPoint, float abilityEffectRange, float abilityDuration, float abilityIncreaseAttackValueInPercentage)
        {
            //Spawn projectile
            var projectile = Instantiate(prefabProjectile, spawnPoint.position, Quaternion.identity, spawnPoint).GetComponent<BuffAttackDamageForSetTimeProjectile>();
            projectile.Init(abilityEffectRange, abilityDuration, abilityIncreaseAttackValueInPercentage);
        }
        
        public IncreaseDamageForSetTimeAbility CreateInstance()
        {
            return new IncreaseDamageForSetTimeAbility(this);
        }
    }
    
    public class IncreaseDamageForSetTimeAbility : UtilityAbility
    {
        public float IncreaseAttackValueInPercentage { get; set; }
        
        public IncreaseDamageForSetTimeAbility(IncreaseDamageForSetTimeAbilityObj abilityObj) : base(abilityObj)
        {
            IncreaseAttackValueInPercentage = abilityObj.IncreaseAttackValueInPercentage;
        }
        
        public override void Cast(Transform spawnPoint, Transform target, Caster caster)
        {
            if (TimeLeft > 0) 
                return;
            StartCooldown = true;
            Casted?.Invoke();
            
            var abilityObj = (IncreaseDamageForSetTimeAbilityObj) AbilityObj;
            abilityObj.Cast(spawnPoint, EffectRange, Duration, IncreaseAttackValueInPercentage);
        }
    }
}