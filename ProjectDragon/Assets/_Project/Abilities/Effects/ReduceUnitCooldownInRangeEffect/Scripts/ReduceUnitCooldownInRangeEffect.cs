using Units.Unit.BaseUnits;
using UnityEngine;

namespace Abilities.Effects.ReduceUnitCooldownInRangeEffect.Scripts
{
    public class ReduceUnitCooldownInRangeEffect : MonoBehaviour
    {
        [SerializeField] private SphereCollider sphereCollider;

        private Unit unit;
        private float percentage;

        public void Init(float value, float range, Unit owner)
        {
            sphereCollider.radius = range;
            percentage = value;
            unit = owner;
        }

        private static void ModifyCooldown(float value, Unit nearby)
        {
            nearby.Ability.CoolDown *= value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Unit")) 
                return;
            var nearby = other.GetComponent<Unit>();
            if (nearby == unit)
                return;
            ModifyCooldown(percentage, nearby);
        }
    }
}