using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class UtilityAbilityDatabase : AbilityDataBase
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

        

        #endregion
    
        #region protected Fields

        [SerializeField] protected float duration;
        [SerializeField] protected float buffValue;

        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties

        public float Duration
        {
            get => duration;
            set => duration = value;
        }

        public float BuffValue
        {
            get => buffValue;
            set => buffValue = value;
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
