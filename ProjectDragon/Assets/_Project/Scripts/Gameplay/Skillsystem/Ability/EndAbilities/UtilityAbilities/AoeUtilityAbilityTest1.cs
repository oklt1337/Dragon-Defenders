using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Skillsystem.Ability;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using Unity.VisualScripting;
using UnityEngine;
using Unit = _Project.Scripts.Gameplay.Unit.Unit;

public class AoeUtilityAbilityTest1 : AoeUtilityAbility
{
    private List<Unit> _possibleBeneficiaries;
    [SerializeField] private GameObject visibleEffect;
    private Vector3 _tempVector;


    public override void Cast()
    {
        Debug.Log(!isCastable);
        if (!isCastable ||_possibleBeneficiaries.Count == 0) return;


        Debug.Log("has received AoeExperience!");
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
                - transform.parent.position.x,
                0,
                _possibleBeneficiaries[i].transform.position.z -
                transform.parent.position.z
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
            _possibleBeneficiaries.Add(allTowers[i].GetComponent<Unit>());   
        }
    }

    private void AddBeneficiaries(Unit newUnit)
    {
    }
    

    protected override void Start()
    {
        base.Start();
        _possibleBeneficiaries = new List<Unit>();
        UpdateBeneficiaries();
    }
}
