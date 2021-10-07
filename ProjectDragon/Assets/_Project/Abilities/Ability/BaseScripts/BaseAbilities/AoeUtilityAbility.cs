using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilities
{
    public class AoeUtilityAbility : UtilityAbility
    {
        
        

        
        #region Singleton

        #endregion
    
        #region SerializeFields
        [ShowInInspector] protected float maxDistance;

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields

    

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

        public override void Start()
        {
            base.Start();
            //maxDistance = ((AoeUtilityAbilityDataBase) abilityDatabase).MAXDistance;
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
            maxDistance = ((AoeUtilityAbilityDataBase) dataBase).MAXDistance;
        }
    

        #endregion
    
        #region CallBacks


        #endregion
    }
}
