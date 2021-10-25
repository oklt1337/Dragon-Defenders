using System.Collections.Generic;
using _Project.SkillSystem.BaseSkills;
using UnityEngine;

namespace _Project.SkillSystem.SkillDataBases
{
    [CreateAssetMenu(menuName="Tools/Skills/SkillSpecialDatabase")]
    public class SkillSpecialDatabase : SkillDataBase
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields

        [SerializeField] protected List<SkillHolderSpecial> skillHolderSpecialList;

        

        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties

    

        #endregion
    
        #region Events

        public List<SkillHolderSpecial> SkillHolderSpecialList => skillHolderSpecialList;

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
