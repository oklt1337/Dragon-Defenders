using System;
using Abilities.Ability.Scripts;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace Abilities.Effects.ReduceDamageAbilityCooldown.Scripts
{
    public class ReduceDamageAbilityCooldownEffect : MonoBehaviour
    {
        private Unit unit;
        private Commander commander;
        private DamageAbility ability;
        private float value;

        public event Action OnEffectDestroyed;

        public void Init(float decreaseValue)
        {
            Debug.Log(decreaseValue);
            
            unit = GetComponent<Unit>();
            commander = GetComponent<Commander>();
            if (unit != null)
            {
                if (!(unit.Ability is DamageAbility damageAbility)) 
                    return;
                
                ability = damageAbility;
                ability.CoolDown *= decreaseValue;
            }
            else if (commander != null)
            {
                if (!(commander.Abilities[0] is DamageAbility damageAbility)) 
                    return;
                
                ability = damageAbility;
                ability.CoolDown *= decreaseValue;
            }
        }

        private void OnDestroy()
        {
            ability.CoolDown = ((DamageAbilityObj) ability.AbilityAbilityObj).CoolDown;
        }

        public void Destroy()
        {
            Destroy(this);
            OnEffectDestroyed?.Invoke();
        }
    }
}