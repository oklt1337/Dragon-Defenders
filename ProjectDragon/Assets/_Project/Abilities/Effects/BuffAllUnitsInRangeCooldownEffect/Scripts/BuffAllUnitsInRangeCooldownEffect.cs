using Abilities.Ability.Scripts;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace Abilities.Effects.BuffAllUnitsInRangeCooldownEffect.Scripts
{
    public class BuffAllUnitsInRangeCooldownEffect : MonoBehaviour
    {
        [SerializeField] private SphereCollider sphereCollider;
        
        private float percentage;

        public void Init(float value, float range)
        {
            sphereCollider.radius = range;
            percentage = value;
        }

        private void ReduceCooldown(Unit unit)
        {
            if (unit == null)
                return;
            unit.Ability.CoolDown *= percentage;
        }

        private void ResetCooldown(Unit unit)
        {
            if (unit == null)
                return;
            unit.Ability.CoolDown /= percentage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Unit")) 
                return;
            ReduceCooldown(other.GetComponent<Unit>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Unit"))
                return;
            ResetCooldown(other.GetComponent<Unit>());
        }
    }
}