using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilities
{
    public class SingleTargetDamageAbility : DamageAbility
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields
        [ShowInInspector] protected float speed;
    

        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties
        public float Speed
        {
            get => speed;
            set => speed = value;
        }
    

        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods
        
        //Von Chris
        public override void Init(AbilityDataBase dataBase)
        {
            base.Init(dataBase);
            speed = ((SingleTargetDamageAbilityDataBase)dataBase).Speed;
        }
        
        public override void Start()
        {
            base.Start();
            //speed = ((SingleTargetDamageAbilityDataBase)abilityDatabase).Speed;
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

    

        #endregion
    
        #region CallBacks


        #endregion
    }
}
