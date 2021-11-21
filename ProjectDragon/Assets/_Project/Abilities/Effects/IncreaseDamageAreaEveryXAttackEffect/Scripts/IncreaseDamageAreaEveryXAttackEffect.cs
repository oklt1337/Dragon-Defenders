using Abilities.EndAbilities.MeleeAttack.Scripts;
using Abilities.Projectiles.Scripts;
using UnityEngine;

namespace Abilities.Effects.IncreaseDamageAreaEveryXAttackEffect.Scripts
{
    public class IncreaseDamageAreaEveryXAttackEffect : MonoBehaviour
    {
        private int counter;
        private int casted;
        private float scale;
        private MeleeAttackAbility meleeAttackAbility;
        private BoxCollider boxCollider;
        private Vector3 size;
        private Vector3 center;

        public void Init(float value, int xTime, MeleeAttackAbility ability)
        {
            meleeAttackAbility = ability;
            scale = value;
            counter = xTime;
            boxCollider = ability.Owner.GetComponentInChildren<MeleeProjectile>().GetComponent<BoxCollider>();
            size = boxCollider.size;
            center = boxCollider.center;
            meleeAttackAbility.Casted += (t) => Counter();
        }

        private void Counter()
        {
            casted++;
            if (casted == counter - 1)
            {
                casted = 0;
                var currentSize = boxCollider.size;
                currentSize = new Vector3(currentSize.x, currentSize.y * scale, currentSize.z);
                boxCollider.size = currentSize;
                var currentCenter = boxCollider.center;
                currentCenter = new Vector3(currentCenter.x, currentCenter.y * scale, currentCenter.z);
                boxCollider.center = currentCenter;
            }
            else if (casted == counter)
            {
                boxCollider.size = size;
                boxCollider.center = center;
            }
        }
    }
}