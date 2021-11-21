using Abilities.Ability.Scripts;
using Abilities.Effects.IncreaseDamageEffect.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using Unit = Units.Unit.BaseUnits.Unit;

namespace Abilities.EndAbilities.IncreaseDamageForSetTime.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/Abilities/UtilityAbilities/IncreaseDamageForSetTime", fileName = "IncreaseDamageForSetTime")]
    public class IncreaseDamageForSetTimeAbilityObj : UtilityAbilityObj
    {
        [SerializeField] private float increaseAttackValueInPercentage;
        public float IncreaseAttackValueInPercentage => increaseAttackValueInPercentage;
        
        public static void Cast(Transform target ,float abilityDuration ,float abilityIncreaseAttackValueInPercentage)
        {
            var effect = target.AddComponent<IncreaseDamageEffect>();
            effect.Init(abilityDuration, abilityIncreaseAttackValueInPercentage);
        }

        public override T CreateInstance<T>()
        {
            return new IncreaseDamageForSetTimeAbility(this) as T;
        }
    }
    
    public class IncreaseDamageForSetTimeAbility : UtilityAbility
    {
        public float IncreaseAttackValueInPercentage { get; set; }
        public Transform CurrenTarget { get; set; }
        
        public IncreaseDamageForSetTimeAbility(IncreaseDamageForSetTimeAbilityObj abilityObj) : base(abilityObj)
        {
            IncreaseAttackValueInPercentage = abilityObj.IncreaseAttackValueInPercentage;
        }

        public override void OnEnter(Transform target)
        {
        }

        public override void OnStay(Transform target)
        {
            if (TimeLeft > 0) 
                return;
            if (target.GetComponent<IncreaseDamageEffect>() != null)
                return;
            if (!(target.GetComponent<Unit>().Ability is DamageAbility)) 
                return;
            
            StartCooldown = true;
            Casted?.Invoke(target);
            CurrenTarget = target;
            IncreaseDamageForSetTimeAbilityObj.Cast(target, Duration, IncreaseAttackValueInPercentage);
        }

        public override void OnExit(Transform target)
        {
        }
    }
}