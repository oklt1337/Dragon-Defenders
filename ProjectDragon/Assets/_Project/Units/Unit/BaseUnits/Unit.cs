using System;
using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.GamePlay.GameManager.Scripts;
using _Project.GamePlay.Player.AnimationHandler.Scripts;
using _Project.GamePlay.Player.SoundHandler.Scripts;
using _Project.Scripts.Gameplay.Faction;
using _Project.Scripts.Gameplay.Skillsystem;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using _Project.Scripts.Gameplay.Unit.UnitDatabases;
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
        [SerializeField] protected BaseUnitDataBase baseUnitDataBase;
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
        private float coolDownModifier;
        private string currentSkillString;
        private string unitPathName = "Resources/Units/";
        private string abilityPath = "Abilities/";
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
            Debug.Log("DataBaseZuweisung here");
            //Debug.Log(typeof(ability));
            Debug.Log(baseUnitDataBase.UnitAbilityDataBase.unitAbilitiesDataBase);
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
            foreach (var skill in skillTree.tree)
            {
                skill.Value.Init();
            }
            
            //skillTree = GetComponentInChildren<SkillTree>();
            //ability = skillTree.allPossibleSkillTree[0];
            //think About This
            
            //offline way also parent again in photon
            //ability = Instantiate(skillTree.AllPossibleSkillTree[0],transform);
            
            //Photon way
            /*
            string loadResourceAbility = "Abilities/" + skillTree.AllPossibleSkillTree[0].gameObject.name;
            //Debug.Log(loadResourceAbility);
            GameObject abilityObject = PhotonNetwork.Instantiate(loadResourceAbility,transform.position,Quaternion.identity);
            ability = abilityObject.GetComponent<Ability>();
            */
            
            
            
            //
            //
            //PhotonNetwork.Instantiate
        }

        [Button]
        public void Dismantle()
        {
            float dividerValue = 0.5f;
            GameManager.Instance.PlayerModel.AddMoney((int)(cost*dividerValue));
            Destroy(this);
        }

        public virtual void Start()
        {
            LoadDataFromScriptableObject();
            InitiateAbility(true);
            InitiateSkillTree();
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
        [Button]
        public void UpgradeSkill(bool isLeftSkill = false)
        {
            if (currentSkillString.Length >= skillTree.maxLayers) return;
            
            String pathTaken =(isLeftSkill) ? "L" : "R" ;
            String.Concat(currentSkillString, pathTaken);
            Skill currentSkill = skillTree.tree[currentSkillString];
            currentSkill.IsLearnable = true;
            currentSkill.EnableSkill();
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
    }
}
