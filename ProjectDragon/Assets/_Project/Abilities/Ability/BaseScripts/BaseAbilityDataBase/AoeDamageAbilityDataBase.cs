using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    [CreateAssetMenu(menuName="Tools/Ability/BaseAbility/AoeDamageAbilityDataBase")]
    public class AoeDamageAbilityDataBase : DamageAbilityDataBase
    {
        #region Singleton

        #endregion
    
        #region SerializeFields
        
    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields
        [SerializeField] protected bool isSpawnProjectileOnEnemyPosition;
        [SerializeField] protected float lifeTime;


        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties

        public float LifeTime
        {
            get => lifeTime;
            set => lifeTime = value;
        }

        public bool IsSpawnProjectileOnEnemyPosition
        {
            get => isSpawnProjectileOnEnemyPosition;
            set => isSpawnProjectileOnEnemyPosition = value;
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