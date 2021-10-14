using System.Collections.Generic;
using _Project.GamePlay.GameManager.Scripts;
using _Project.SkillSystem.SkillDataBases;
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
                case SkillEnum.Attack:
                    GameManager.Instance.PlayerModel.Commander.AttackDamageModifier += skillHolder.SkillIncreaseValue;
                    break;
            
                case SkillEnum.Defense:
                    GameManager.Instance.PlayerModel.Commander.Defense += skillHolder.SkillIncreaseValue;
                    break;
                case SkillEnum.Health:
                    GameManager.Instance.PlayerModel.Commander.MAXHealth += skillHolder.SkillIncreaseValue;
                    break;
                case SkillEnum.Experience:
                    //
                    //GameManager.Instance.PlayerModel.Commander.Experience += skillHolder.SkillIncreaseValue;
                    Debug.Log("Experience was private set last time i checked");
                    break;
                case SkillEnum.Gold:
                    GameManager.Instance.PlayerModel.ModifyMoney((int)skillHolder.SkillIncreaseValue);
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
}
