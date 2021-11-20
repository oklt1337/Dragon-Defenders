using Abilities.Ability.Scripts;
using AI.Enemies.Base_Enemy.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace Abilities.Effects.DamageOverTimeEffect.Scripts
{
    public class DamageOverTimeEffect : MonoBehaviour
    {
        private Enemy enemy;
        private float duration;
        private float tick;
        private float fixTick;
        private float value;
        private bool casted;

        public void Init(float effectDuration, float damageValue, float damageTicker)
        {
            enemy = GetComponent<Enemy>();
            duration = effectDuration;
            value = damageValue;
            tick = damageTicker;
            fixTick = tick;
        }

        private void Update()
        {
            if (duration > 0)
            {
                DamageTicker();
            }
            else
            {
                DurationCounter();
            }
        }

        private void DamageTicker()
        {
            tick -= Time.deltaTime;
            if (!(tick <= 0)) 
                return;
            DealDamage();
        }

        private void DurationCounter()
        {
            duration -= Time.deltaTime;
            if (!(duration <= 0)) 
                return;
            Destroy(this);
        }

        private void DealDamage()
        {
            tick = fixTick;
            enemy.TakeDamage(value);
        }
    }
}