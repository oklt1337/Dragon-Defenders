using System.Collections;
using System.Collections.Generic;
using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.GamePlay.GameManager.Scripts;
using _Project.SkillSystem.SkillDataBases;
using _Project.Units.Unit.BaseUnits;
using UnityEngine;

namespace _Project.SkillSystem.BaseSkills
{
    public class SkillSpecial : Skill
    {
        protected List<SkillHolderSpecial> SkillHolderSpecialList; 
        public override bool EnableSkill()
        {
            if (!IsLearnable || IsSkillActive) return false;
            //if (GameManager.Instance.PlayerModel.Money <= cost) return false;
            
            foreach (var skillHolderSpecial in SkillHolderSpecialList)
            {
                ActivateSkill(skillHolderSpecial);
            }
            
            IsSkillActive = true;
            IsLearnable = false;
            return true;
        }

        private void ActivateSkill(SkillHolderSpecial skillHolderSpecial)
        {
            //many are still private set change it when the time comes
            switch (skillHolderSpecial.SkillEnumSpecial)
            {
                default:
                    ActivateTowerSkill(skillHolderSpecial);
                    break;
            }
        }

        
        private void ActivateTowerSkill(SkillHolderSpecial skillHolderSpecial)
        {
            switch (skillHolderSpecial.SkillEnumSpecial)
            {
                case SkillEnumSpecial.BlessingOfTheTrees:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            int necessaryForBlessingOfTheTreesValueLenght = 3;
                            if(skillHolderSpecial.SkillIncreaseValues.Length <= necessaryForBlessingOfTheTreesValueLenght)
                                continue;
                            
                            float waitTime = skillHolderSpecial.SkillIncreaseValues[0];
                            float upTime = skillHolderSpecial.SkillIncreaseValues[1];
                            float increaseValue = skillHolderSpecial.SkillIncreaseValues[2];

                            unit.StartCoroutine(BlessingOfTheTreesCoroutine(unit, waitTime, upTime,increaseValue));
                        }
                    } 
                    break;
                case SkillEnumSpecial.ScreamOfTheWild:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            int necessaryForBlessingOfTheTreesValueLenght = 1;
                            if(skillHolderSpecial.SkillIncreaseValues.Length <= necessaryForBlessingOfTheTreesValueLenght)
                                continue;
                            
                            float numberOfAttacksNeeded = skillHolderSpecial.SkillIncreaseValues[0];
                            float scale = skillHolderSpecial.SkillIncreaseValues[1];
                            ((AoeDamageAbility)unit.Ability).UnlockScreamOfTheWild((int)numberOfAttacksNeeded, scale);
                        }
                    } 
                    break;
                default:
                    break;
            }
        }
        public override void Init()
        {
            base.Init();
            SkillHolderSpecialList = ((SkillSpecialDatabase)skillDataBase).SkillHolderSpecialList;
        }
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

        private IEnumerator BlessingOfTheTreesCoroutine(OldUnit oldUnit ,float waitSeconds, float activeSeconds, float increaseValue )
        {
            float originalValueSpeed = ((SingleTargetDamageAbility)oldUnit.Ability).Speed;
            float originalValueCooldown = ((SingleTargetDamageAbility)oldUnit.Ability).Speed;
            float modifiedValueSpeed = originalValueSpeed * increaseValue;
            float modifiedValueCooldown = originalValueSpeed / increaseValue;
            
            while(oldUnit.gameObject.activeSelf)
            {
                ((SingleTargetDamageAbility) oldUnit.Ability).Speed = originalValueSpeed;
                ((SingleTargetDamageAbility) oldUnit.Ability).Cooldown = originalValueCooldown;
                
                yield return new WaitForSeconds(waitSeconds);
                
                ((SingleTargetDamageAbility) oldUnit.Ability).Speed = modifiedValueSpeed;
                ((SingleTargetDamageAbility) oldUnit.Ability).Speed = modifiedValueCooldown;
                
                yield return new WaitForSeconds(activeSeconds);
            }
        }

        #endregion
    
        #region protected Fields

    

        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties

    

        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods

    

        #endregion
    
        #region Private Methods

    

        #endregion
    
        #region Protected Methods

    

        #endregion
    
        #region Public Methods

    

        #endregion
    
        #region CallBacks


        #endregion
    }
}
