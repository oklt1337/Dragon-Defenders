using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    [CreateAssetMenu(menuName="Tools/Ability/BaseAbility/SkillShotDamageAbilityDataBase")]
    public class SkillShotDamageAbilityDataBase : SingleTargetDamageAbilityDataBase
    {
        
        #region Singleton

        #endregion
    
        #region SerializeFields

        

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields

        [SerializeField] protected float maxProjectileRange;
        [SerializeField] protected int bulletsPerCast;
        [SerializeField] protected float angleOffset;

        #endregion
    
        #region Public Fields

        

        #endregion
    
        #region Public Properties

        public float MaxProjectileRange
        {
            get => maxProjectileRange;
            set => maxProjectileRange = value;
        }

        public int BulletsPerCast
        {
            get => bulletsPerCast;
            set => bulletsPerCast = value;
        }

        public float AngleOffset
        {
            get => angleOffset;
            set => angleOffset = value;
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
