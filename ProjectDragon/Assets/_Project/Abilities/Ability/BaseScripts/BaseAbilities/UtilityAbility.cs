using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilities
{
    public abstract class UtilityAbility: Ability
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields
        [ShowInInspector] protected float duration;
        [ShowInInspector] protected float buffValue;

    

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
        public override void Start()
        {
            base.Start();
            //duration = ((UtilityAbilityDatabase) abilityDatabase).Duration;
            //buffValue = ((UtilityAbilityDatabase) abilityDatabase).BuffValue;
        }

        public override void Update()
        {
            base.Update();
        }
    

        #endregion
    
        #region Private Methods
        
    

        #endregion
    
        #region Protected Methods

    

        #endregion
    
        #region Public Methods
        public override void Init(AbilityDataBase dataBase)
        {
            base.Init(dataBase);
            duration = ((UtilityAbilityDatabase) dataBase).Duration;
            buffValue = ((UtilityAbilityDatabase) dataBase).BuffValue;
        }
    

        #endregion
    
        #region CallBacks


        #endregion
    }
}
