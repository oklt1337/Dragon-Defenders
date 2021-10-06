using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities
{
    public abstract class DamageAbility : Abilities.Ability.BaseScripts.BaseAbilities.Ability
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

        public float BaseDamage
        {
            get => baseDamage;
            set => baseDamage = value;
        }

        public GameObject DamageProjectile
        {
            get => damageProjectile;
            set => damageProjectile = value;
        }

        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods
        public override void Start()
        {
            base.Start();
            baseDamage = ((DamageAbilityDataBase)abilityDatabase).BaseDamage;
            damageProjectile = ((DamageAbilityDataBase)abilityDatabase).DamageProjectile;
            
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
