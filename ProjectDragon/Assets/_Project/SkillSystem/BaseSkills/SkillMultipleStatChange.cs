using System.Collections.Generic;
using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Faction;
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
        private ClassAndFaction.Class newClass;

        
        public override bool EnableSkill()
        {
            if (!IsLearnable || IsSkillActive) return false;
            //if (GameManager.Instance.PlayerModel.ModifyMoney(cost)) return false;
            
            foreach (var skillHolder in SkillHolderList)
            {
                ActivateSkill(skillHolder);
            }

            if (newClass != ClassAndFaction.Class.None)
                ChangeFaction();
                
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
                        ability.ManaCost *= skillHolder.SkillIncreaseValue;
                    }
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
                case SkillEnum.LifeTime:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            ((AoeDamageAbility)unit.Ability).LifeTime *= skillHolder.SkillIncreaseValue;
                        }
                    }
                    break;
                case SkillEnum.MaxProjectileRange:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            ((SkillShotDamageAbility)unit.Ability).MaxProjectileRange *= skillHolder.SkillIncreaseValue;
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
                case SkillEnum.BulletsPerCast:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            Debug.Log(unit.Ability.GetType().ToString());
                            ((SkillShotDamageAbility)(unit.Ability)).BulletsPerCast = (int)skillHolder.SkillIncreaseValue;
                            
                        }
                    }
                    break;
                case SkillEnum.AngleOffset:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            
                            ((SkillShotDamageAbility)unit.Ability).AngleOffset = skillHolder.SkillIncreaseValue;
                        }
                    }
                    break;
                case SkillEnum.KnockBack:
                    foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
                    {
                        if (unit.SkillTree.tree.ContainsValue(this))
                        {
                            if (((DamageAbility)unit.Ability).Knockback == 0)
                            {
                                ((DamageAbility) unit.Ability).Knockback = 1;
                            }
                            ((DamageAbility)unit.Ability).Knockback *= skillHolder.SkillIncreaseValue;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private  void ChangeFaction()
        {
            foreach (var unit in GameManager.Instance.UnitManager.ActiveUnits)
            {
                if (unit.SkillTree.tree.ContainsValue(this))
                {
                    unit.UnitClass = newClass;
                }
            }
        }

        public override void Init()
        {
            base.Init();
            SkillHolderList = ((SkillMultipleStatChangeDataBase)skillDataBase).SkillHolderList;
            newClass= ((SkillMultipleStatChangeDataBase)skillDataBase).NewClass;
        }
        
        public ClassAndFaction.Class NewClass
        {
            get => newClass;
            set => newClass = value;
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
