using System.Collections;
using System.Collections.Generic;
using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Abilities.Ability.EndAbilities.UtilityAbilities.AoeUtilityAbilityTest1
{
    public class AoeUtilityAbilityTest1 : AoeUtilityAbility
    {
        private List<_Project.Units.Unit.BaseUnits.Unit> _possibleBeneficiaries;
        [SerializeField] private GameObject visibleEffect;
        private Vector3 _tempVector;


        public override void Cast()
        {
            if (!isCastable ||_possibleBeneficiaries.Count == 0) return;
        
            CastEffect();

            ResetCoolDown();
        }

        private void CastEffect()
        {
            for (int i = 0; i < _possibleBeneficiaries.Count; i++)
            {
                //set both y variables to zero to have a buff Cylinder
                _tempVector = new Vector3(
                    _possibleBeneficiaries[i].transform.position.x
                    - transform.position.x,
                    0,
                    _possibleBeneficiaries[i].transform.position.z -
                    transform.position.z
                );
                if(_tempVector.sqrMagnitude <= maxDistance*maxDistance)
                    _possibleBeneficiaries[i].GainExp(BuffValue);
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
            _possibleBeneficiaries.Clear();
            GameObject[] allTowers = GameObject.FindGameObjectsWithTag("Unit/Tower");
            for (int i = 0;i < allTowers.Length;i++)
            {
                _possibleBeneficiaries.Add(allTowers[i].GetComponent<_Project.Units.Unit.BaseUnits.Unit>());   
            }
        }

        private void AddBeneficiaries(Unit newUnit)
        {
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
            _possibleBeneficiaries = new List<_Project.Units.Unit.BaseUnits.Unit>();
            UpdateBeneficiaries();
        }
    }
}
