using System.Collections.Generic;
using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.GamePlay.GameManager.Scripts;
using _Project.SkillSystem.SkillDataBases;
using _Project.Units.Unit.BaseUnits;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.SkillSystem.BaseSkills
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class SkillMultipleStatChange : Skill
    {
        protected List<SkillHolder> SkillHolderList; 
        public override bool EnableSkill()
        {
            if (!IsLearnable || IsSkillActive) return false;
            
            foreach (var skillHolder in SkillHolderList)
            {
                ActivateSkill(skillHolder);
            }
            
            IsSkillActive = true;
            IsLearnable = false;
            return true;
        }

        private void ActivateSkill(SkillHolder skillHolder)
        {

            //many are still private set change it when the time comes
            switch (skillHolder.SkillEnum)
            {
                case SkillEnum.CommanderAttack:
                    GameManager.Instance.PlayerModel.Commander.AttackDamageModifier *= skillHolder.SkillIncreaseValue;
                    break;
                case SkillEnum.CommanderDefense:
                    GameManager.Instance.PlayerModel.Commander.Defense *= skillHolder.SkillIncreaseValue;
                    break;
                case SkillEnum.CommanderHealth:
                    GameManager.Instance.PlayerModel.Commander.MAXHealth *= skillHolder.SkillIncreaseValue;
                    break;
                case SkillEnum.CommanderManaCost:
                    foreach (var ability in  GameManager.Instance.PlayerModel.Commander.Abilities)
                    {
                        ability.Cooldown *= skillHolder.SkillIncreaseValue;
                    }
                    Debug.Log("Experience was private set last time i checked");
                    break;
                case SkillEnum.CommanderExperience:
                    //GameManager.Instance.PlayerModel.Commander.Experience += skillHolder.SkillIncreaseValue;
                    Debug.Log("Experience was private set last time i checked");
                    break;
                case SkillEnum.CommanderCoolDown:
                    foreach (var ability in GameManager.Instance.PlayerModel.Commander.Abilities)
                    {
                        ability.Cooldown *= skillHolder.SkillIncreaseValue;
                    }
                    break;
                case SkillEnum.CommanderGold:
                    GameManager.Instance.PlayerModel.ModifyMoney((int)skillHolder.SkillIncreaseValue);
                    break;
                default:
                    ActivateTowerSkill(skillHolder);
                    break;
            }
        }

        
        private void ActivateTowerSkill(SkillHolder skillHolder)
        {
            switch (skillHolder.SkillEnum)
            {
                //Tower
                case SkillEnum.CoolDown:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            unit.Ability.Cooldown *= skillHolder.SkillIncreaseValue;
                        }
                    }
                    break;
                case SkillEnum.BaseDamage:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            ((DamageAbility)unit.Ability).BaseDamage *= skillHolder.SkillIncreaseValue;
                        }
                    }
                    break;
                case SkillEnum.DamageModifier:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            ((Combat)unit).AttackDamageModifier *= skillHolder.SkillIncreaseValue;
                        }
                    }
                    break;
                case SkillEnum.Speed:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            ((SingleTargetDamageAbility)unit.Ability).Speed *= skillHolder.SkillIncreaseValue;
                        }
                    }
                    break;
                case SkillEnum.Duration:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            ((AoeUtilityAbility)unit.Ability).Duration *= skillHolder.SkillIncreaseValue;
                        }
                    }
                    break;
                case SkillEnum.BuffValue:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            ((AoeUtilityAbility)unit.Ability).BuffValue *= skillHolder.SkillIncreaseValue;
                        }
                    }
                    break;
                case SkillEnum.MaxDistance:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            ((AoeDamageAbility)unit.Ability).MAXDistance *= skillHolder.SkillIncreaseValue;
                        }
                    }
                    break;
                case SkillEnum.MaxProjectileRange:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            ((SkillshotDamageAbility)unit.Ability).MaxProjectileRange *= skillHolder.SkillIncreaseValue;
                        }
                    }
                    break;
                case SkillEnum.AttackRange:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            ((Combat)unit).AttackRange *= skillHolder.SkillIncreaseValue;
                            ((Combat)unit).GetComponent<SphereCollider>().radius = ((Combat)unit).AttackRange;
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
            SkillHolderList = ((SkillMultipleStatChangeDataBase)skillDataBase).SkillHolderList;
        }
    }
    
    #region Singleton

    #endregion
    
    #region SerializeFields

    

    #endregion
    
    #region Private Fields

    

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
