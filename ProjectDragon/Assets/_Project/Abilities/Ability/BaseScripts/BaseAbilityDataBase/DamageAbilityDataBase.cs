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

        [SerializeField] protected float baseDamage;
        [SerializeField] protected float knockback;

       

        [SerializeField] protected GameObject damageProjectile;

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
        
        public float Knockback
        {
            get => knockback;
            set => knockback = value;
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
