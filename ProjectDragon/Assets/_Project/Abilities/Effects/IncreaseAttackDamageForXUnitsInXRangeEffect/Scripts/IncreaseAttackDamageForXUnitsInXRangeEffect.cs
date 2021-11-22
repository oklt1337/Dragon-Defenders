using System.Collections.Generic;
using Abilities.Ability.Scripts;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace Abilities.Effects.IncreaseAttackDamageForXUnitsInXRangeEffect.Scripts
{
    public class IncreaseAttackDamageForXUnitsInXRangeEffect : MonoBehaviour
    {
        [SerializeField] private SphereCollider sphereCollider;

        private Commander commander;
        private float damage;

        public void Init(float value, float range, Commander owner)
        {
            sphereCollider.radius = range;
            damage = value;
            commander = owner;
        }

        private void ModifyDamage(float value)
        {
            foreach (var ability in commander.Abilities)
            {
                if (ability is DamageAbility damageAbility)
                {
                    damageAbility.Damage += value;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Unit")) 
                return;
            ModifyDamage(damage);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Unit"))
                return;
            ModifyDamage(-damage);
        }
    }
}