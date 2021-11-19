using Abilities.Ability.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace Abilities.Effects.IncreaseDamageEffect.Scripts
{
    public class IncreaseDamageEffect : MonoBehaviour
    {
        private Unit unit;
        private DamageAbility ability;
        private float value;
        private float duration;
        private bool casted;

        public void Init(float buffDuration, float increaseValue)
        {
            unit = GetComponent<Unit>();
            if (!(unit.Ability is DamageAbility damageAbility)) 
                return;
            
            unit.Ability.Casted += () => casted = true;
            ability = damageAbility;
            ability.Damage *= increaseValue;
            duration = buffDuration;
        }

        private void Update()
        {
            duration -= Time.deltaTime;
            if (!(duration <= 0) || !casted) 
                return;
            Destroy(this);
        }

        private void OnDestroy()
        {
            ability.Damage = ((DamageAbilityObj) ability.AbilityAbilityObj).Damage;
        }
    }
}