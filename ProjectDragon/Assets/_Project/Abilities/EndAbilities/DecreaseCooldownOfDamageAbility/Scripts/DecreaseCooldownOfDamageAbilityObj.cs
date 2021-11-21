using System;
using System.Collections.Generic;
using System.Linq;
using Abilities.Ability.Scripts;
using Abilities.Effects.ReduceDamageAbilityCooldown.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Unit = Units.Unit.BaseUnits.Unit;

namespace Abilities.EndAbilities.DecreaseCooldownOfDamageAbility.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/Abilities/UtilityAbilities/DecreaseCooldownOfDamage", fileName = "DecreaseCooldownOfDamage")]
    public class DecreaseCooldownOfDamageAbilityObj : UtilityAbilityObj
    {
        [SerializeField] private float decreaseCooldownValueInPercentage;
        public float DecreaseCooldownValueInPercentage => decreaseCooldownValueInPercentage;

        public static void Cast(Transform target, float value)
        {
            var unit = target.GetComponent<Unit>();
            if (unit != null)
            {
                if (!(unit.Ability is DamageAbility)) 
                    return;
                AddEffect(unit, value);
            }
            else
            {
                AddEffect(target, value);
            }
        }

        private static void AddEffect(Object target, float value)
        {
            var effect = target.AddComponent<ReduceDamageAbilityCooldownEffect>();
            effect.Init(value);
        }

        public override T CreateInstance<T>()
        {
            return new DecreaseCooldownOfDamageAbility(this) as T;
        }
    }

    public class DecreaseCooldownOfDamageAbility : UtilityAbility
    {
        public float DecreaseCooldownValueInPercentage { get; set; }
        public float MaxTargets { get; set; }
        public event Action<Transform> OnTargetChanged;
        
        private readonly List<Transform> units = new List<Transform>();
        private readonly List<Transform> lastTargets = new List<Transform>();

        public DecreaseCooldownOfDamageAbility(DecreaseCooldownOfDamageAbilityObj abilityObj) : base(abilityObj)
        {
            DecreaseCooldownValueInPercentage = abilityObj.DecreaseCooldownValueInPercentage;
            MaxTargets = 1;
        }

        public override void OnEnter(Transform target)
        {
            if (!target.CompareTag("Unit"))
                return;
            units.Add(target);
        }

        public override void OnStay(Transform target)
        {
            SelectTarget(target);
        }

        public override void OnExit(Transform target)
        {
            if (!target.CompareTag("Player") && !target.CompareTag("Unit")) 
                return;
            
            if (lastTargets.Contains(target))
            {
                RemoveBuffOfOldTarget(target);
            }
            if (units.Contains(target))
            {
                units.Remove(target);
            }
        }

        private void SelectTarget(Transform target)
        {
            if (lastTargets.Count < MaxTargets)
                lastTargets.Add(target);

            if (target.CompareTag("Player"))
            {
                SetPlayerAsTarget(target);
            }
            else if (target.CompareTag("Unit"))
            {
                SetUnitAsTarget();
            }
        }

        private void SetPlayerAsTarget(Transform target)
        {
            if (lastTargets.Contains(target))
            {
                if (target.GetComponent<ReduceDamageAbilityCooldownEffect>() != null)
                    return;
                DecreaseCooldownOfDamageAbilityObj.Cast(target, DecreaseCooldownValueInPercentage);
            }
            else
            {
                DecreaseCooldownOfDamageAbilityObj.Cast(target, DecreaseCooldownValueInPercentage);
                RemoveBuffOfOldTarget(lastTargets[0]);
                OnTargetChanged?.Invoke(target);
            }
        }

        private void SetUnitAsTarget()
        {
            var closest = units.OrderBy(unit => Vector3.Distance(unit.position, Owner.position)).ToArray();
            if (lastTargets.Contains(closest.First()))
            {
                if (closest.First().GetComponent<ReduceDamageAbilityCooldownEffect>() != null)
                    return;
                DecreaseCooldownOfDamageAbilityObj.Cast(closest.First(), DecreaseCooldownValueInPercentage);
            }
            else
            {
                var index = lastTargets.FindIndex(target => !target.CompareTag("Player"));
                if (index == -1)
                    return;
                RemoveBuffOfOldTarget(lastTargets[index]);
                lastTargets.Add(closest.First());
                if (closest.First().GetComponent<ReduceDamageAbilityCooldownEffect>() != null)
                    return;
                DecreaseCooldownOfDamageAbilityObj.Cast(closest.First(), DecreaseCooldownValueInPercentage);
                OnTargetChanged?.Invoke(closest.First());
            }
        }

        private void RemoveBuffOfOldTarget(Transform target)
        {
            if (!lastTargets.Contains(target)) 
                return;
            lastTargets.Remove(target);
            var component = target.GetComponent<ReduceDamageAbilityCooldownEffect>();
            if (component != null)
                component.Destroy();
        }
    }
}