using System.Collections.Generic;
using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Units.Unit.BaseUnitDatabases;
using UnityEngine;

namespace _Project.Units.Unit.BaseUnits
{
    public abstract class Combat : Unit
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
            //transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y, transform.rotation.z);

            /*if (!currentTarget.gameObject.activeSelf)
            {
                SelectTarget();
            }
            else
            {
                ability.Cast(transform,currentTarget);
            }
            */
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

        #endregion
    
        #region CallBacks


        #endregion
    }
}
