using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.Ability.Scripts
{
    public abstract class UtilityAbilityObj : AbilityObj
    {
        [SerializeField] private float effectRange;
        [SerializeField] private float duration;
        
        [SerializeField] protected GameObject prefabProjectile;
        public float EffectRange => effectRange;
        public float Duration => duration;
    }
    
    public class UtilityAbility : Ability
    {
        public float EffectRange { get; set; }
        public float Duration { get; set; }
        
        public UtilityAbility(UtilityAbilityObj abilityObj) : base(abilityObj)
        {
            EffectRange = abilityObj.EffectRange;
            Duration = abilityObj.Duration;
        }

        public override void Cast(Transform spawnPoint, Transform target, Caster caster)
        {
            if (TimeLeft > 0) 
                return;
            StartCooldown = true;
            Casted?.Invoke();
        }
    }
}
