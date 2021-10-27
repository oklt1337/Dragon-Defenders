using System;
using System.Collections.Generic;
using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.GamePlay.GameManager.Scripts;
using _Project.GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using _Project.Units.Unit.BaseUnits;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilities
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class AoeUtilityAbility : UtilityAbility
    {
        
        #region Singleton

        #endregion
    
        #region SerializeFields
        [ShowInInspector] protected float maxDistance;

        

        #endregion
    
        #region Private Fields
        protected Vector3 tempVector;
    

        #endregion
    
        #region protected Fields
        protected List<Unit> possibleBeneficiariesUnits;
        protected List<Commander> possibleBeneficiariesCommander;
        
    

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

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, maxDistance);
        }

        #endregion
    
        #region Private Methods

        private void CastEffect()
        {
            for (int i = 0; i < possibleBeneficiariesUnits.Count; i++)
            {
                //set both y variables to zero to have a buff Cylinder
                tempVector = new Vector3(
                    possibleBeneficiariesUnits[i].transform.position.x
                    - transform.position.x,
                    0,
                    possibleBeneficiariesUnits[i].transform.position.z -
                    transform.position.z
                );
                if(tempVector.sqrMagnitude <= maxDistance*maxDistance)
                    possibleBeneficiariesUnits[i].GainExp(BuffValue);
            }
        }

        private void ActivateBuffUnit(Unit unit)
        {
            
        }
        
        private void ActivateBuffCommander(Unit unit)
        {
            
        }

        #endregion
    
        #region Protected Methods

        

        #endregion
    
        #region Public Methods
        public override void Init(AbilityDataBase dataBase)
        {
            base.Init(dataBase);
            maxDistance = ((AoeUtilityAbilityDataBase) dataBase).MAXDistance;
            if (GameManager.Instance)
            {
                possibleBeneficiariesUnits = GameManager.Instance.UnitManager.ActiveUnits;
                possibleBeneficiariesCommander = new List<Commander>(){GameManager.Instance.PlayerModel.Commander};
            }
        }
        
        public override void Cast()
        {
            if (!isCastable || possibleBeneficiariesUnits.Count == 0) return;
        
            CastEffect();

            ResetCoolDown();
        }

        #endregion
    
        #region CallBacks


        #endregion
    }
}
