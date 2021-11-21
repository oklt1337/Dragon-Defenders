using Abilities.Ability.Scripts;
using UnityEngine;

namespace Abilities.Effects.DecreaseDamageEveryXAttackEffect.Scripts
{
    public class DecreaseDamageEveryXAttackEffect : MonoBehaviour
    {
        private int counter;
        private int casted;
        private float modifier;
        private DamageAbility damageAbility;
        private float damage;

        public void Init(float value, int xTime, DamageAbility ability)
        {
            damageAbility = ability;
            modifier = value;
            counter = xTime;
            damageAbility.Casted += (t) => Counter();
            damage = ability.Damage;
        }

        private void Counter()
        {
            casted++;
            if (casted == counter - 1)
            {
                damageAbility.Damage *= modifier;
            }
            else if (casted == counter)
            {
                casted = 0;
                damageAbility.Damage = damage;
            }
        }
    }
}