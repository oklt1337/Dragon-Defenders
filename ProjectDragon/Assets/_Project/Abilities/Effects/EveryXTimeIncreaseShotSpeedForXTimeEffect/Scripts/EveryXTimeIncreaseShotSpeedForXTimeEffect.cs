using Abilities.EndAbilities.SingleShot.Scripts;
using UnityEngine;

namespace Abilities.Effects.EveryXTimeIncreaseShotSpeedForXTimeEffect.Scripts
{
    public class EveryXTimeIncreaseShotSpeedForXTimeEffect : MonoBehaviour
    {
        private SingleShotAbility ability;
        private float oldSpeed;
        private float value;
        private float buffDuration;
        private float buffTimer;
        private float recycleDuration;
        private float recycleTimer;
        private bool startBuffTimer;

        public void Init(SingleShotAbility newAbility, float newRecycleDuration, float newBuffDuration,
            float increaseValue)
        {
            //Set parameter
            ability = newAbility;
            value = increaseValue;
            recycleDuration = newRecycleDuration;
            buffDuration = newBuffDuration;
            buffTimer = buffDuration;
            recycleTimer = recycleDuration;
            oldSpeed = ability.ProjectileSpeed;

            ApplyEffect();
        }

        private void Update()
        {
            if (startBuffTimer)
            {
                BuffTimer();
            }
            else
            {
                RecycleTimer();
            }
        }

        private void RecycleTimer()
        {
            recycleTimer -= Time.deltaTime;
            if (!(recycleTimer <= 0))
                return;
            ApplyEffect();
        }

        private void BuffTimer()
        {
            buffTimer -= Time.deltaTime;
            if (!(buffTimer <= 0))
                return;
            ResetBuff();
        }

        private void ResetBuff()
        {
            ability.ProjectileSpeed = oldSpeed;
            buffTimer = buffDuration;
            startBuffTimer = false;
        }

        private void ApplyEffect()
        {
            ability.ProjectileSpeed *= value;
            recycleTimer = recycleDuration;
            startBuffTimer = true;
        }
    }
}