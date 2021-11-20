using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.Ability.Scripts
{
    public abstract class UtilityAbilityObj : AbilityObj
    {
        [SerializeField] private float effectRange;
        [SerializeField] private float duration;
        [SerializeField] private bool handleEnter;
        [SerializeField] private bool handleStay;
        [SerializeField] private bool handleExit;
        
        public float EffectRange => effectRange;
        public float Duration => duration;
        public bool HandleEnter => handleEnter;
        public bool HandleStay => handleStay;
        public bool HandleExit => handleExit;
    }
    
    public abstract class UtilityAbility : Ability
    {
        public float EffectRange { get; set; }
        public float Duration { get; set; }
        public bool HandleEnter { get; set; }
        public bool HandleStay { get; set; }
        public bool HandleExit { get; set; }
        
        public UtilityAbility(UtilityAbilityObj abilityObj) : base(abilityObj)
        {
            EffectRange = abilityObj.EffectRange;
            Duration = abilityObj.Duration;
            HandleEnter = abilityObj.HandleEnter;
            HandleStay = abilityObj.HandleStay;
            HandleExit = abilityObj.HandleExit;
        }

        public abstract void OnEnter(Transform target);
        public abstract void OnStay(Transform target);
        public abstract void OnExit(Transform target);
    }
}
