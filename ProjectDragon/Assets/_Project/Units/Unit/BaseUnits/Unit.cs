using System;
using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Faction;
using _Project.GamePlay.GameManager.Scripts;
using _Project.GamePlay.Player.AnimationHandler.Scripts;
using _Project.GamePlay.Player.SoundHandler.Scripts;
using _Project.Scripts.Gameplay.Skillsystem;
using _Project.SkillSystem.BaseSkills;
using _Project.SkillSystem.SkillTree;
using _Project.Units.Unit.BaseUnitDatabases;
using Photon.Pun;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace _Project.Units.Unit.BaseUnits
{
    public abstract class Unit : MonoBehaviour
    {   
        #region Singleton

        #endregion
    
        #region SerializeFields

        [SerializeField] protected BaseUnitDataBase baseUnitDataBase;

        #endregion
    
        #region Private Fields

        private float coolDownModifier;
        private string currentSkillString;
        private string unitPathName = "Units/";
        private string abilityPath = "Abilities/";

        #endregion
    
        #region protected Fields

        [ShowInInspector] protected string unitName; 
        [ShowInInspector] protected string description;
        [ShowInInspector] protected GameObject unitModel;
        [ShowInInspector] protected Factions.Faction faction;
        [ShowInInspector] protected Factions.Class unitClass;

        

        [ShowInInspector] protected byte rank;
        [ShowInInspector] protected int cost;
        
        [ShowInInspector] protected SkillTree skillTree;

        
        [ShowInInspector] protected Ability ability;

        

        [ShowInInspector] protected AnimationHandler animationHandler;
        [ShowInInspector] protected SoundHandler soundHandler;

        [ShowInInspector] protected byte level;
        [ShowInInspector] protected float experience;
        [ShowInInspector] protected float cooldown;
        
        #endregion
    
        #region Public Fields

        

        #endregion
    
        #region Public Properties
        
        public string UnitPathName
        {
            get => unitPathName;
            set => unitPathName = value;
        }
        public byte Level
        {
            get => level;
            set => level = value;
        }

        public float Experience
        {
            get => experience;
            set => experience = value;
        }

        public int Cost
        {
            get => cost;
            set => cost = value;
        }
        
        public SkillTree SkillTree
        {
            get => skillTree;
            set => skillTree = value;
        }

        public Ability Ability
        {
            get => ability;
            set => ability = value;
        }
        
        public Factions.Class UnitClass
        {
            get => unitClass;
            set => unitClass = value;
        }

        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods

        public virtual void Start()
        {
            GameManager.Instance.UnitManager.SubscribeUnit(this);
            LoadDataFromScriptableObject();
            InitiateAbility(true);
            InitiateSkillTree();
        }
        
        public virtual void OnDestroy()
        {
            GameManager.Instance.UnitManager.UnsubscribeUnit(this);
        }

        #endregion
    
        #region Private Methods

    

        #endregion
    
        #region Protected Methods

        protected virtual void LoadDataFromScriptableObject()
        {
            unitName = baseUnitDataBase.UnitName;
            description = baseUnitDataBase.Description;
            unitModel = baseUnitDataBase.UnitModel;
            faction = baseUnitDataBase.Faction;
            unitClass = baseUnitDataBase.UnitClass;
            rank = baseUnitDataBase.Rank;
            cost = baseUnitDataBase.Cost;

            skillTree = baseUnitDataBase.SkillTree;
            animationHandler = baseUnitDataBase.AnimationHandler;
            soundHandler = baseUnitDataBase.SoundHandler;


            if(ability != null)cooldown = ability.Cooldown;
        }
        
        protected virtual void InitiateAbility(bool resetCoolDownModifier = false)
        {
            ability = (Ability) gameObject.AddComponent(baseUnitDataBase.UnitAbilityDataBase.UnitAbilitiesScript);
            ability.Init(baseUnitDataBase.UnitAbilityDataBase.unitAbilitiesDataBase);
            if (resetCoolDownModifier)
            {
                float startModifier = 1;
                coolDownModifier = startModifier;
                cooldown = ability.Cooldown;
            }
            else
            {
                cooldown = ability.Cooldown * coolDownModifier;
            }
        }
        protected virtual void InitiateSkillTree()
        {
            skillTree.InitSkillTreeSkills();
        }

        protected virtual void ApplyModifiers()
        {
            //still things to add;
            ability.Cooldown = cooldown * coolDownModifier;
        }
        
        protected void Cast()
        {
            ability.Cast();
        }
        
        protected virtual void Update()
        {
            //ability.Update = 
            //ability.Update();
        }
        
        #endregion
    
        #region Public Methods

        [Button]
        public void Dismantle()
        {
            float dividerValue = 0.5f;
            GameManager.Instance.PlayerModel.ModifyMoney((int)(cost*dividerValue));
            Destroy(this);
        }
        
        public void GainExp(float gainedExp)
        {
            experience += gainedExp;
        }
        
        public virtual void LevelUp()
        {
            level++;
            
            //increase Stats
            
            //thinking on setting is learnable here
            
            ApplyModifiers();

        }
        //outdated must be renewed based on the new skillsystem
        public void UpgradeSkill(string skillTreeKey)
        {
            SkillTree.EnableSkillThroughSkillTree(skillTreeKey);
        }
        

        #endregion
    
        #region CallBacks


        #endregion
        
        

        
        
       

        

        

        
        
        
    }
}
