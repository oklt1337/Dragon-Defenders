using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilities
{
    public class SkillshotDamageAbility : SingleTargetDamageAbility
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields
        [ShowInInspector] protected float maxDistance;
    

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
            //maxDistance = ((SkillShotDamageAbilityDataBase)abilityDatabase).MAXDistance;
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
            maxDistance = ((SkillShotDamageAbilityDataBase) dataBase).MAXDistance;
        }
    

        #endregion
    
        #region CallBacks


        #endregion
    }
}
