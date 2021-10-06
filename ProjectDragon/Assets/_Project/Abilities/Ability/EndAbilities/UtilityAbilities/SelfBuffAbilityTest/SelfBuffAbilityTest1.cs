using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using _Project.Units.Unit.BaseUnits;
using UnityEngine;

namespace _Project.Abilities.Ability.EndAbilities.UtilityAbilities.SelfBuffAbilityTest
{
    public class SelfBuffAbilityTest1 : SelfBuffAbility
    {
        
        //private Commander Commander 
        private Unit _unit;
        
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
          _unit = GetComponentInParent<Unit>();
        }

        private void CastEffect(Unit target)
        {
            target.GainExp(BuffValue);
        }
    }
}