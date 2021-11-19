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
            units.Add(target);
        }

        public override void OnStay(Transform target)
        {
            if (CheckIfPlayerIsInRange(target))
                return;
            if (units.Count == 0)
                return;
            if (!target.CompareTag("Unit"))
                return;
            SelectUnit();
        }

        public override void OnExit(Transform target)
        {
            if (!target.CompareTag("Player"))
            {
                if (target == lastTarget)
                    RemoveBuffOfOldTarget();
                return;
            }
            
            if (!target.CompareTag("Unit"))
                return;
            if (target == lastTarget)
                RemoveBuffOfOldTarget();
            
            units.Remove(target);
        }

        private bool CheckIfPlayerIsInRange(Component other)
        {
            if (!other.CompareTag("Player")) 
                return false;
            if (lastTarget == other.transform)
            {
                if (lastTarget.GetComponent<ReduceDamageAbilityCooldownEffect>() != null)
                    return true;
            }
            else
            {
                RemoveBuffOfOldTarget();
                //Set new Target
                lastTarget = other.transform;
            }
            DecreaseCooldownOfDamageAbilityObj.Cast(lastTarget, DecreaseCooldownValueInPercentage);
            OnTargetChanged?.Invoke(lastTarget);
            return true;
        }
        
        private void SelectUnit()
        {
            var closest = units.OrderBy(unit => Vector3.Distance(unit.position, Owner.position)).ToArray();
            if (lastTarget == closest.First())
            {
                DecreaseCooldownOfDamageAbilityObj.Cast(lastTarget, DecreaseCooldownValueInPercentage);
            }
            else
            {
                RemoveBuffOfOldTarget();
                //Set new Target
                lastTarget = closest.First();
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