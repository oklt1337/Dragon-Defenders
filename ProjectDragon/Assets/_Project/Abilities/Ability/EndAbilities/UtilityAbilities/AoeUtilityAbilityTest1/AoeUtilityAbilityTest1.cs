using System.Collections;
using System.Collections.Generic;
using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using Unity.VisualScripting;
using UnityEngine;
using Unit = _Project.Units.Unit.BaseUnits.Unit;

namespace _Project.Abilities.Ability.EndAbilities.UtilityAbilities.AoeUtilityAbilityTest1
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class AoeUtilityAbilityTest1 : AoeUtilityAbility
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

        [SerializeField] private GameObject visibleEffect;

        #endregion
    
        #region Private Fields

        private List<_Project.Units.Unit.BaseUnits.OldUnit> possibleBeneficiaries;
        
        private Vector3 tempVector;

        #endregion
    
        #region protected Fields

    

        #endregion
    
        #region Public Fields

        

        #endregion
    
        #region Public Properties

    

        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods

    

        #endregion
    
        #region Private Methods

        private void CastEffect()
        {
            for (int i = 0; i < possibleBeneficiaries.Count; i++)
            {
                //set both y variables to zero to have a buff Cylinder
                tempVector = new Vector3(
                    possibleBeneficiaries[i].transform.position.x
                    - transform.position.x,
                    0,
                    possibleBeneficiaries[i].transform.position.z -
                    transform.position.z
                );
                if(tempVector.sqrMagnitude <= maxDistance*maxDistance)
                    possibleBeneficiaries[i].GainExp(BuffValue);
            }

            if (visibleEffect)
            {
                StartCoroutine(VisibleDisplay());
            }
        }
        
        private IEnumerator VisibleDisplay()
        {
            visibleEffect.SetActive(true);
            yield return new WaitForSeconds(duration);
            visibleEffect.SetActive(false);
        
        }

        private void UpdateBeneficiaries()
        {
            possibleBeneficiaries.Clear();
            GameObject[] allTowers = GameObject.FindGameObjectsWithTag("Unit/Tower");
            for (int i = 0;i < allTowers.Length;i++)
            {
                possibleBeneficiaries.Add(allTowers[i].GetComponent<_Project.Units.Unit.BaseUnits.OldUnit>());   
            }
        }

        private void AddBeneficiaries(Unit newUnit)
        {
        }

        #endregion
    
        #region Protected Methods

    

        #endregion
    
        #region Public Methods

        public override void Cast()
        {
            if (!isCastable ||possibleBeneficiaries.Count == 0) return;
        
            CastEffect();

            ResetCoolDown();
        }
        
        public override void Start()
        {
            base.Start();
            //_possibleBeneficiaries = new List<_Project.Units.Unit.BaseUnits.Unit>();
            //UpdateBeneficiaries();
        }
        
        public override void Init(AbilityDataBase dataBase)
        {
            base.Init(dataBase);
            possibleBeneficiaries = new List<_Project.Units.Unit.BaseUnits.OldUnit>();
            UpdateBeneficiaries();
        }

        #endregion
    
        #region CallBacks


        #endregion
    }
}
