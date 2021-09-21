﻿using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability
{
    public class SelfBuffAbilityTest1 : SelfBuffAbility
    {
        
        //private Commander Commander 
        private Unit.Unit _unit;
        
        public override void Cast()
        {
            Debug.Log("SelfBuff");
            if (!isCastable) return;

            if (_unit)
            {
                Debug.Log(_unit.name + "has received Experience!");
                CastEffect(_unit);
                //duration will be implemented when the active buff 
                //system is implemented.
            }
            else
            {
                GetTarget();
            }
            
            ResetCoolDown();
        }

        private void GetTarget()
        {
          _unit = GetComponentInParent<Unit.Unit>();
        }

        private void CastEffect(Unit.Unit target)
        {
            target.GainExp(BuffValue);
        }
    }
}