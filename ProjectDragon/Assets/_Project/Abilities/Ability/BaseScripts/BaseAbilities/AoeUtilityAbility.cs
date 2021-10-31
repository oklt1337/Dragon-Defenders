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
        
        [ShowInInspector] protected bool isCaster;
        [ShowInInspector] protected bool isPassiveBuffer;
        

        
        

        #endregion
    
        #region Private Fields


        #endregion
    
        #region protected Fields
        protected List<OldUnit> possibleBeneficiariesUnits;
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
            if (!isCaster) return;
            
            foreach (var unit in possibleBeneficiariesUnits)
            {
                if(CheckRange(unit.transform.position))
                    ActivateBuffUnit(unit);
            }
            foreach (var commander in possibleBeneficiariesCommander)
            {
                if(CheckRange(commander.transform.position))
                    ActivateBuffCommander(commander);
            }

        }
        
        private void ProvidePassiveEffect()
        {
            if (!isPassiveBuffer) return;
            
            foreach (var unit in possibleBeneficiariesUnits)
            {
                if(CheckRange(unit.transform.position))
                    ActivateBuffUnit(unit);
            }
            foreach (var commander in possibleBeneficiariesCommander)
            {
                if(CheckRange(commander.transform.position))
                    ActivateBuffCommander(commander);
            }

        }

        private bool CheckRange(Vector3 otherPosition)
        {
            Vector3 tmpVector = new Vector3(
                otherPosition.x - transform.position.x,
                0,
                otherPosition.z - transform.position.z);
            return (tmpVector.sqrMagnitude <= maxDistance * maxDistance);

        }

        private void ActivateBuffUnit(OldUnit oldUnit)
        {
            
        }
        
        private void ActivateBuffCommander(Commander commander)
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
        /*
        ·         (400) Minor Buff: cooldown is reduced by 50%
        ·         (400) Loud Cheer: Increases Range to 5
        ·         (800) Super Buff: cooldown is reduced by 3/4
        ·         (800) Dual Buffing: Spell helps 2 closest now
        ·         (800) Heal ray: If Commander is in range he will get a health regeneration by .5% Max Health/seconds
        */
    }
}
