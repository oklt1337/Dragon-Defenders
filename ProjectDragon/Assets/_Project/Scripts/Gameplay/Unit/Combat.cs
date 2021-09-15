using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Enemies;
using _Project.Scripts.Gameplay.Unit.UnitDatabases;
using ExitGames.Client.Photon.Encryption;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Unit
{
    public abstract class Combat : Unit
    {
        public float attackDamageModifier;
        public float attackRange;
        
        public Transform currentTarget;
        public List<Transform> targets;
        private bool _isEnemyInRange;


        //private LoadInfoDataBase
        protected override void LoadDataFromScriptableObject()
        {
            base.LoadDataFromScriptableObject();
            CombatUnitDataBase tmpDataBase = ((CombatUnitDataBase) baseUnitDataBase);
            attackDamageModifier = tmpDataBase.attackDamageModifier;
            attackRange = tmpDataBase.attackRange;
        }
        
        protected void SelectTarget()
        {
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
            //Debug.Log("Combat triggerExit");
            //Debug.Log(other.CompareTag("Enemy"));
            
            if (other.CompareTag("Enemy"))
            {
                targets.Remove(other.transform);
                //Debug.Log(other.transform == currentTarget);
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
        
        
        public void Ability()
        {
            
        }
        
        public virtual void Update()
        {
            /*
            if (!currentTarget)
            {
                Debug.Log("Combat not Cast");
                SelectTarget();
            }
            else
            {
                Debug.Log("Combat Cast");
                ability.Cast();
            }
            */
            
        }


        //update with select // 
        
        
    }
}
