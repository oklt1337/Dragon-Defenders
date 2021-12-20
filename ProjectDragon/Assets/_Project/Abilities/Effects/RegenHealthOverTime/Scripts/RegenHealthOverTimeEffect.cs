using System;
using Abilities.Ability.Scripts;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace Abilities.Effects.RegenHealthOverTime.Scripts
{
    public class RegenHealthOverTimeEffect : MonoBehaviour
    {
        private Commander commander;
        private float regenValue;
        private float tick;
        private float coolDown;
        
        public void Init(float value, float tickValue)
        {
            commander = GetComponent<Commander>();
            regenValue = value;
            tick = tickValue;
            coolDown = tick;
        }

        private void Update()
        {
            coolDown -= Time.deltaTime;
            if (coolDown <= 0)
                RegenHealth();
        }

        private void RegenHealth()
        {
            coolDown = tick;
            if (commander != null)
            {
                commander.CommanderStats.Health += commander.CommanderStats.MaxHealth * regenValue;
            }
        }

        public void Destroy()
        {
            Destroy(this);
        }
    }
}