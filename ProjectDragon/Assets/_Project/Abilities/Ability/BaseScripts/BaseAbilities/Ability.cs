using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilities
{
    public abstract class Ability : MonoBehaviour
    {
        
        
        

        
        
        
        
        
        #region Singleton
        
        #endregion
    
        #region SerializeFields
        
        [SerializeField] protected AbilityDataBase abilityDatabase;
        
        #endregion
    
        #region Private Fields
        #endregion
    
        #region protected Fields
        
        [ShowInInspector] protected float manaCost;
        [ShowInInspector] protected float cooldown;
        [ShowInInspector] protected AnimationClip animationClip;
        [ShowInInspector] protected AudioClip audioClip;
        [ShowInInspector] private float _tempCooldown;
        [ShowInInspector] protected bool isCastable;
        [ShowInInspector] protected string projectilepath = "Resources/Projectiles/";
        
        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties
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
    

        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods
        public virtual void Start()
        {
            manaCost = abilityDatabase.ManaCost;
            cooldown = abilityDatabase.Cooldown;
            _tempCooldown = 0;
            isCastable = true;
        }

        public virtual void Update()
        {
            _tempCooldown -= Time.deltaTime;
            if (_tempCooldown <= 0)
            {
                isCastable = true;
            }
        }
        #endregion
    
        #region Private Methods

    

        #endregion
    
        #region Protected Methods
        protected virtual void ResetCoolDown()
        {
            isCastable = false;
            _tempCooldown = cooldown;
        }
    

        #endregion
    
        #region Public Methods

        public virtual void Cast()
        {
            
        }
        
        public virtual void Cast(Transform target)
        {
            
        }
        public virtual void Cast(Transform spawnPosition, Transform enemy)
        {
            
        }
        
        public virtual void ResetCoolDown(float newCoolDown)
        {
            isCastable = false;
            cooldown = newCoolDown;
            _tempCooldown = newCoolDown;
        }
        #endregion
    
        #region CallBacks


        #endregion
    }
}
