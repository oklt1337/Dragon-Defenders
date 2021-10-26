using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilities
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public abstract class DamageAbility : Ability
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields

        [ShowInInspector] protected float baseDamage;
        [ShowInInspector] protected float knockback;
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
        
        public float Knockback
        {
            get => knockback;
            set => knockback = value;
        }
        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods

        

        public override void Start()
        {
            base.Start();
            //baseDamage = ((DamageAbilityDataBase)abilityDatabase).BaseDamage;
            //damageProjectile = ((DamageAbilityDataBase)abilityDatabase).DamageProjectile;
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
        //Von Chris
        public override void Init(AbilityDataBase dataBase)
        {
            base.Init(dataBase);
            baseDamage = ((DamageAbilityDataBase)dataBase).BaseDamage;
            damageProjectile = ((DamageAbilityDataBase)dataBase).DamageProjectile;
            knockback = ((DamageAbilityDataBase)dataBase).Knockback;
        }
    

        #endregion
    
        #region CallBacks


        #endregion
    }
}
