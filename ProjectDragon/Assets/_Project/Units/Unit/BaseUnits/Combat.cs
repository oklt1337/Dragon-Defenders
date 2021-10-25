using System.Collections;
using System.Collections.Generic;
using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Units.Unit.BaseUnitDatabases;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Units.Unit.BaseUnits
{
    public class Combat : Unit
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields
        
        protected float attackDamageModifier;
        protected float attackRange;
        
        protected Transform currentTarget;
        protected List<Transform> targets;
        
        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties

        public float AttackDamageModifier
        {
            get => attackDamageModifier;
            set => attackDamageModifier = value;
        }

        public float AttackRange
        {
            get => attackRange;
            set => attackRange = value;
        }

        public Transform CurrentTarget
        {
            get => currentTarget;
            set => currentTarget = value;
        }

        public List<Transform> Targets
        {
            get => targets;
            set => targets = value;
        }

        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods

        protected override void Update()
        {
            base.Update();
            if (!currentTarget) 
            {
                SelectTarget();
                return;
            }
            transform.rotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
            CastAbilityIfPossible();
        }

        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                targets.Add(other.transform);
            }
            
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                targets.Remove(other.transform);
                if (other.transform == currentTarget)
                {
                    SelectTarget();
                }
            }
        }

        public override void Start()
        {
            base.Start();
            targets = new List<Transform>();
            LoadDataFromScriptableObject();
            GetComponent<SphereCollider>().radius = attackRange;
        }

        #endregion
    
        #region Private Methods

        

        #endregion
    
        #region Protected Methods

        protected override void LoadDataFromScriptableObject()
        {
            base.LoadDataFromScriptableObject();
            CombatUnitDataBase tmpDataBase = ((CombatUnitDataBase) baseUnitDataBase);
            attackDamageModifier = tmpDataBase.AttackDamageModifier;
            attackRange = tmpDataBase.AttackRange;
        }
        
        protected override void ApplyModifiers()
        {
            ((DamageAbility)ability).BaseDamage *= attackDamageModifier;
            ability.Cooldown = cooldown;
        }
        
        protected void SelectTarget()
        {
            if (targets.Count == 0)
            {
                currentTarget = null;
                return;
            } 
            float smallestDistance  = float.MaxValue;
            foreach (Transform enemyTransform in targets)
            {
                if (smallestDistance >= (transform.position - enemyTransform.position).sqrMagnitude)
                {
                    smallestDistance = (transform.position - enemyTransform.position).sqrMagnitude;
                    currentTarget = enemyTransform;
                    
                }
            }
        }
        
        protected virtual void CastAbilityIfPossible()
        {
            if (!ability.IsCastable)
                return;
            //cast Check is inside the ability
            if (!currentTarget || !currentTarget.gameObject.activeSelf)
            {
                SelectTarget();
            }
            else
            {
                ability.Cast(transform,currentTarget);
            }
            
        }

        #endregion
    
        #region Public Methods

        public override void LevelUp()
        {
            level++;
            
            //skilltree new increase
            //UpgradeSkill();
            
            //cooldown gets smaller 
            //cooldown *= 0.95f;
            
            //Modifierincreased
            //attackDamageModifier *= 1.1f;
            
            //apply new modifiers
            ApplyModifiers();
        }

        #region SpecialSkills

        /*public bool BlessingOfTheTrees(float waitSeconds, float activeSeconds, float increaseValue)
        {
            StartCoroutine(BlessingOfTheTreesCoroutine(waitSeconds,activeSeconds,increaseValue));
            return true;
        }

        private IEnumerator BlessingOfTheTreesCoroutine(float waitSeconds, float activeSeconds, float increaseValue )
        {
            float originalValueSpeed = ((SingleTargetDamageAbility)ability).Speed;
            float originalValueCooldown = ((SingleTargetDamageAbility)ability).Speed;
            float modifiedValueSpeed = originalValueSpeed * increaseValue;
            float modifiedValueCooldown = originalValueSpeed * increaseValue;
            
            while(true)
            {
                ((SingleTargetDamageAbility) ability).Speed = originalValueSpeed;
                ((SingleTargetDamageAbility) ability).Cooldown = originalValueCooldown;
                
                yield return new WaitForSeconds(waitSeconds);
                
                ((SingleTargetDamageAbility) ability).Speed = modifiedValueSpeed;
                ((SingleTargetDamageAbility) ability).Speed = modifiedValueCooldown;
                
                yield return new WaitForSeconds(activeSeconds);
            }
        }
        */

        #endregion
        #endregion
    
        #region CallBacks


        #endregion
    }
}
