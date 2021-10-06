using System;
using _Project.SkillSystem.SkillDataBases;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.SkillSystem.BaseSkills
{
    [System.Serializable]
    public abstract class Skill
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

        [SerializeField] protected SkillDataBase skillDataBase;
        [ShowInInspector] protected string skillName;
        [ShowInInspector][TextArea] protected string description;
        [ShowInInspector] protected Sprite sprite;
        
        

        

        #endregion
    
        #region Private Fields

        private bool isSkillActive;
        private bool isLearnable;

        #endregion

        #region protected Fields


        #endregion

        #region Public Fields



        #endregion

        #region Public Properties

        public bool IsSkillActive
        {
            get => isSkillActive;
            set => isSkillActive = value;
        }
        
        public Sprite Sprite
        {
            get => sprite;
            set => sprite = value;
        }
        public bool IsLearnable
        {
            get => isLearnable;
            set => isLearnable = value;
        }
        
        public string Name
        {
            get => skillName;
            set => skillName = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        #endregion

        #region Events



        #endregion

        #region Unity Methods

        

        #endregion

        #region Private Methods



        #endregion

        #region Protected Methods
        
        public abstract bool EnableSkill();

        

        #endregion

        #region Public Methods

        public virtual void Init()
        {
            isSkillActive = false;
            isLearnable = false;
            sprite = skillDataBase.Sprite;
            skillName = skillDataBase.SkillName;
            description = skillDataBase.Description;
        }

        #endregion

        #region CallBacks


        #endregion
    }

    public enum SkillEnum
    {
        None,
        Health,
        Attack,
        Defense,
        Experience,
        Gold,
    }
    [System.Serializable]
    public class SkillHolder
    {
        [SerializeField]private SkillEnum skillEnum;
        [SerializeField]private float skillIncreaseValue;
        public SkillEnum SkillEnum => skillEnum;

        public float SkillIncreaseValue => skillIncreaseValue;
        
        
    }
}
