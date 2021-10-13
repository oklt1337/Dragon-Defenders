using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class AbilityDataBase : ScriptableObject
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields
        
        [SerializeField] protected float manaCost;
        [SerializeField] protected float cooldown;
        [SerializeField] protected AnimationClip animationClip;
        [SerializeField] protected AudioClip audioClip;
    

        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties

        public float ManaCost
        {
            get => manaCost;
            private set => manaCost = value;
        }

        public AnimationClip AnimationClip
        {
            get => animationClip;
            private set => animationClip = value;
        }

        public AudioClip AudioClip
        {
            get => audioClip;
            private set => audioClip = value;
        }

        public float Cooldown
        {
            get => cooldown;
            private set => cooldown = value;
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
