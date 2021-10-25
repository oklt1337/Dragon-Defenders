using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class AoeUtilityAbilityDataBase : UtilityAbilityDatabase
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

        

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields

        [SerializeField] protected float maxDistance;

        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties

        public float MAXDistance
        {
            get => maxDistance;
            set => maxDistance = value;
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
