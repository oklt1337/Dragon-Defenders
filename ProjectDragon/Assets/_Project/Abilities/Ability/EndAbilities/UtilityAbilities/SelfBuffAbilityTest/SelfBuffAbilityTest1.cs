using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.Units.Unit.BaseUnits;
using UnityEngine;

namespace _Project.Abilities.Ability.EndAbilities.UtilityAbilities.SelfBuffAbilityTest
{
    public class SelfBuffAbilityTest1 : SelfBuffAbility
    {
        
        //private Commander Commander 
        private OldUnit oldUnit;
        
        public override void Cast()
        {
            Debug.Log("SelfBuff");
            if (!isCastable) return;

            if (oldUnit)
            {
                Debug.Log(oldUnit.name + "has received Experience!");
                CastEffect(oldUnit);
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
          oldUnit = GetComponentInParent<OldUnit>();
        }

        private void CastEffect(OldUnit target)
        {
            target.GainExp(BuffValue);
        }
        
        public override void Init(AbilityDataBase dataBase)
        {
            base.Init(dataBase);
        }
    }
}