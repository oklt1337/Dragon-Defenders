using System.Collections.Generic;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using _Project.Scripts.Gameplay.Unit.UnitDatabases;
using UnityEngine;

namespace _Project.Units.Unit.BaseUnits
{
    public abstract class Combat : Unit
    {
        public float attackDamageModifier;
        public float attackRange;
        
        public Transform currentTarget;
        public List<Transform> targets;

        protected override void LoadDataFromScriptableObject()
        {
            base.LoadDataFromScriptableObject();
            CombatUnitDataBase tmpDataBase = ((CombatUnitDataBase) baseUnitDataBase);
            attackDamageModifier = tmpDataBase.attackDamageModifier;
            attackRange = tmpDataBase.attackRange;
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
        
        protected override void ApplyModifiers()
        {
            ((DamageAbility)ability).BaseDamage *= attackDamageModifier;
            ability.Cooldown = cooldown;
        }

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
    }
}
