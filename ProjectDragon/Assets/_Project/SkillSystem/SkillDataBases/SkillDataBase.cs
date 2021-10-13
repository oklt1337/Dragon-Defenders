using UnityEngine;

namespace _Project.SkillSystem.SkillDataBases
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public abstract class SkillDataBase : ScriptableObject
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

        

        #endregion
    
        #region protected Fields

        [SerializeField]protected Sprite sprite;
        [SerializeField] protected string skillName;
        [SerializeField][TextArea] protected string description;
        

        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties

        public Sprite Sprite
        {
            get => sprite;
            set => sprite = value;
        }
        public string SkillName 
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

    

        #endregion
    
        #region Public Methods

    

        #endregion
    
        #region CallBacks


        #endregion
        
    }
}
