using System;
using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] protected AbilityDataBase abilityDatabase;
        [SerializeField] protected float manaCost;
        [SerializeField] protected float cooldown;
        [SerializeField] protected AnimationClip animationClip;
        [SerializeField] protected AudioClip audioClip;
        private float _tempCooldown;
        protected bool isCastable;
        public float ManaCost
        {
            get => manaCost;
            set => manaCost = value;
        }

        public float Cooldown
        {
            get => cooldown;
            set => cooldown = value;
        }
        public float TempCooldown
        {
            get => _tempCooldown;
            set => _tempCooldown = value;
        }
         
        public bool IsCastable
        {
            get => isCastable;
            set => isCastable = value;
        }
        protected virtual void Start()
        {
            manaCost = abilityDatabase.ManaCost;
            cooldown = abilityDatabase.Cooldown;
            _tempCooldown = 0;
            isCastable = true;
        }

        protected virtual void Update()
        {
            _tempCooldown -= Time.deltaTime;
            if (_tempCooldown <= 0)
            {
                isCastable = true;
            }
        }

        protected virtual void ResetCoolDown()
        {
            isCastable = false;
            _tempCooldown = cooldown;
        }

        public virtual void Cast()
        {
            
        }
        
        public virtual void Cast(Transform target)
        {
            
        }
        public virtual void Cast(Transform spawnPosition, Transform enemy)
        {
            
        }
    }
}
