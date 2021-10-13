using System.Collections.Generic;
using _Project.SkillSystem.BaseSkills;
using UnityEngine;

namespace _Project.SkillSystem.SkillDataBases
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    [CreateAssetMenu(menuName="Tools/Skills/SkillMultipleStatChangeDataBase")]
    public class SkillMultipleStatChangeDataBase : SkillDataBase
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields

        [SerializeField] protected List<SkillHolder> skillHolderList;

        

        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties

    

        #endregion
    
        #region Events

        public List<SkillHolder> SkillHolderList => skillHolderList;

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
