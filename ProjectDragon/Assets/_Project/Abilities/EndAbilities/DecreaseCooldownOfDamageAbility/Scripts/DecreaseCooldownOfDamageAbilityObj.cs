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
    [CreateAssetMenu(menuName = "Tools/Abilities/DecreaseCooldownOfDamage", fileName = "DecreaseCooldownOfDamage")]
    public class DecreaseCooldownOfDamageAbilityObj : UtilityAbilityObj
    {
        [SerializeField] private float decreaseCooldownValueInPercentage;
        public float DecreaseCooldownValueInPercentage => decreaseCooldownValueInPercentage;

        public static void Cast(Transform target, float value)
        {
            if (target.GetComponent<ReduceDamageAbilityCooldownEffect>() != null)
                return;
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
        public event Action<Transform> OnTargetChanged;
        
        private readonly List<Transform> units = new List<Transform>();
        private Transform lastTarget;

        public DecreaseCooldownOfDamageAbility(DecreaseCooldownOfDamageAbilityObj abilityObj) : base(abilityObj)
        {
            DecreaseCooldownValueInPercentage = abilityObj.DecreaseCooldownValueInPercentage;
        }
        
        public override void Init(Transform owner)
        {
            Owner = owner;
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
            
            if (target == lastTarget)
            {
                RemoveBuffOfOldTarget();
            }
            if (units.Contains(target))
            {
                units.Remove(target);
            }
        }

        private void SelectTarget(Transform target)
        {
            if (lastTarget == null)
                lastTarget = target;

            if (target.CompareTag("Player"))
            {
                SetPlayerAsTarget(target);
            }
            else if (target.CompareTag("Unit"))
            {
                if (lastTarget.CompareTag("Player"))
                    return;
                SetUnitAsTarget();
            }
        }

        private void SetPlayerAsTarget(Transform target)
        {
            if (lastTarget == target)
            {
                if (target.GetComponent<ReduceDamageAbilityCooldownEffect>() != null)
                    return;
                DecreaseCooldownOfDamageAbilityObj.Cast(target, DecreaseCooldownValueInPercentage);
            }
            else
            {
                DecreaseCooldownOfDamageAbilityObj.Cast(target, DecreaseCooldownValueInPercentage);
                RemoveBuffOfOldTarget();
                OnTargetChanged?.Invoke(target);
            }
        }

        private void SetUnitAsTarget()
        {
            var closest = units.OrderBy(unit => Vector3.Distance(unit.position, Owner.position)).ToArray();
            if (lastTarget == closest.First())
            {
                if (lastTarget.GetComponent<ReduceDamageAbilityCooldownEffect>() != null)
                    return;
                DecreaseCooldownOfDamageAbilityObj.Cast(lastTarget, DecreaseCooldownValueInPercentage);
            }
            else
            {
                RemoveBuffOfOldTarget();
                lastTarget = closest.First();
                if (lastTarget.GetComponent<ReduceDamageAbilityCooldownEffect>() != null)
                    return;
                DecreaseCooldownOfDamageAbilityObj.Cast(lastTarget, DecreaseCooldownValueInPercentage);
                OnTargetChanged?.Invoke(closest.First());
            }
        }

        private void RemoveBuffOfOldTarget()
        {
            if (lastTarget == null) 
                return;
            var component = lastTarget.GetComponent<ReduceDamageAbilityCooldownEffect>();
            if (component != null)
                component.Destroy();
        }
    }
}