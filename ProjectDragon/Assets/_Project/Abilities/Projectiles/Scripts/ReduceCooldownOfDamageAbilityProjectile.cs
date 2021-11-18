using System;
using System.Collections.Generic;
using System.Linq;
using Abilities.Effects.ReduceDamageAbilityCooldown.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public class ReduceCooldownOfDamageAbilityProjectile : UtilityProjectile
    {
        private float value;
        private readonly List<Transform> units = new List<Transform>();
        private Transform target;
        public event Action<Transform> OnTargetChanged;

        public void Init(Transform owner ,float effectRange, float buffValue)
        {
            ((SphereCollider) myCollider).radius = effectRange;
            value = buffValue;
            Owner = owner;
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (Vector3.Distance(other.transform.position, Owner.position) < 1.5)
                return;
            if (!other.CompareTag("Unit"))
                return;
            units.Add(other.transform);
        }

        protected void OnTriggerStay(Collider other)
        {
            if (Vector3.Distance(other.transform.position, Owner.position) < 1.5)
                return;
            if (CheckIfPlayerIsInRange(other))
                return;
            if (units.Count == 0)
                return;
            if (!other.CompareTag("Unit"))
                return;
            SelectUnit();
        }

        protected void OnTriggerExit(Collider other)
        {
            if (Vector3.Distance(other.transform.position, Owner.position) < 1.5)
                return;
            if (!other.CompareTag("Player"))
            {
                if (other.transform == target)
                    RemoveBuffOfOldTarget();
                return;
            }
            
            if (!other.CompareTag("Unit"))
                return;
            if (other.transform == target)
                RemoveBuffOfOldTarget();
            
            units.Remove(other.transform);
        }

        private void RemoveBuffOfOldTarget()
        {
            if (target != null)
            {
                var component = target.GetComponent<ReduceDamageAbilityCooldownEffect>();
                if (component != null)
                    Destroy(component);
            }
        }

        private bool CheckIfPlayerIsInRange(Component other)
        {
            if (!other.CompareTag("Player")) 
                return false;
            if (target == other.transform)
            {
                if (target.GetComponent<ReduceDamageAbilityCooldownEffect>() != null)
                    return true;
            }
            else
            {
                RemoveBuffOfOldTarget();
                //Set new Target
                target = other.transform;
            }
            var effect = target.AddComponent<ReduceDamageAbilityCooldownEffect>();
            effect.Init(value);
            OnTargetChanged?.Invoke(target);
            return true;
        }

        private void SelectUnit()
        {
            var closest = units.OrderBy(enemy => Vector3.Distance(enemy.position, transform.position)).ToArray();
            if (target == closest.First())
            {
                if (target.GetComponent<ReduceDamageAbilityCooldownEffect>() != null)
                    return;
                var effect = target.AddComponent<ReduceDamageAbilityCooldownEffect>();
                effect.Init(value);
                OnTargetChanged?.Invoke(target);
            }
            else
            {
                RemoveBuffOfOldTarget();
                //Set new Target
                target = closest.First();
            }
        }
    }
}