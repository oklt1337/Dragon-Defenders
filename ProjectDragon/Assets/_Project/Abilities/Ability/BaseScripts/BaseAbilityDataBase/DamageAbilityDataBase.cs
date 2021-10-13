using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class DamageAbilityDataBase : AbilityDataBase
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields

        [ShowInInspector] protected float baseDamage;
        [ShowInInspector] protected GameObject damageProjectile;

        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties

        public GameObject DamageProjectile
        {
            get => damageProjectile;
            set => damageProjectile = value;
        }

        public float BaseDamage
        {
            get => baseDamage;
            set => baseDamage = value;
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
