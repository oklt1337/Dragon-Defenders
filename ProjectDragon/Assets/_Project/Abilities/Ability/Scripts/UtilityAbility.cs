using Abilities.Projectiles.Scripts;
using UnityEngine;

namespace Abilities.Ability.Scripts
{
    public abstract class UtilityAbilityObj : AbilityObj
    {
        [SerializeField] private float effectRange;
        
        [SerializeField] private GameObject prefabProjectile;
        [SerializeField] private UtilityProjectile utilityProjectile;
        public float EffectRange => effectRange;
        public UtilityAbility CreateInstance()
        {
            return new UtilityAbility(this);
        }

        public void Cast(Transform spawnPoint, float abilityEffectRange)
        {
            //Spawn projectile
            utilityProjectile = Instantiate(prefabProjectile, spawnPoint.position, Quaternion.identity, spawnPoint).GetComponent<UtilityProjectile>();
            utilityProjectile.Init(abilityEffectRange);
        }
    }
    
    public class UtilityAbility : Ability
    {
        public float EffectRange { get; set; }

        public UtilityAbility(UtilityAbilityObj abilityObj) : base(abilityObj)
        {
            EffectRange = abilityObj.EffectRange;
        }
        
        public void Cast(Transform spawnPoint)
        {
            var utilityAbilityObj = (UtilityAbilityObj) AbilityObj;
            utilityAbilityObj.Cast(spawnPoint, EffectRange);
        }
    }
}
