using System.Security.Cryptography;
using _Project.Scripts.Animation;
using _Project.Scripts.Gameplay.Faction;
using _Project.Scripts.Gameplay.Skillsystem;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using _Project.Scripts.Gameplay.Unit.UnitDatabases;
using _Project.Scripts.Sound;
using Photon.Pun;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Unit
{
    public abstract class Unit : MonoBehaviour
    {   
        [SerializeField] protected BaseUnitDataBase baseUnitDataBase;
        [SerializeField] protected string unitName; 
        [SerializeField] protected GameObject unitModel;
        [SerializeField] protected Factions.Faction faction;
        [SerializeField] protected Factions.Class unitClass;
        [SerializeField] protected byte rank;
        [SerializeField] protected int cost;
        
        [SerializeField] protected float cooldown;
        [SerializeField] protected SkillTree skillTree;
        [SerializeField] protected Ability ability;
        [SerializeField] protected AnimationHandler animationHandler;
        [SerializeField] protected SoundHandler soundHandler;

        [SerializeField] protected byte level;
        [SerializeField] protected float experience;
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

        
        
        protected virtual void LoadDataFromScriptableObject()
        {
            unitName = baseUnitDataBase.name;
            faction = baseUnitDataBase.faction;
            unitClass = baseUnitDataBase.unitClass;
            rank = baseUnitDataBase.rank;
            cost = baseUnitDataBase.cost;
        }

        protected virtual void SetSkillTree()
        {
            skillTree = GetComponentInChildren<SkillTree>();
            //ability = skillTree.allPossibleSkillTree[0];
            //think About This
            
            //offline way also parent again in photon
            ability = Instantiate(skillTree.AllPossibleSkillTree[0],transform);
            
            //Photon way
            /*
            string loadResourceAbility = "Abilities/" + skillTree.AllPossibleSkillTree[0].gameObject.name;
            //Debug.Log(loadResourceAbility);
            GameObject abilityObject = PhotonNetwork.Instantiate(loadResourceAbility,transform.position,Quaternion.identity);
            ability = abilityObject.GetComponent<Ability>();
            */
            cooldown = ability.Cooldown;
            
            //
            //
            //PhotonNetwork.Instantiate
        }
        
        private void Dismantle()
        {
            //When it is ready;
            //GameManager.Instance.PlayerModel.AddMoney(cost/2);
            Destroy(this);
        }

        public virtual void Start()
        {
            LoadDataFromScriptableObject();
            SetSkillTree();
        }

        public void GainExp(float gainedExp)
        {
            experience += gainedExp;
            if (experience >= 100)
            {
                LevelUp();
                experience %= 100;
            }
        }

        protected virtual void LevelUp()
        {
            level++;
            
            //skilltree new increase
            UpgradeSkill();
            
            //cooldown gets smaller 
            cooldown *= 0.95f;
            
            //apply new modifiers
            ApplyModifiers();

        }

        public void UpgradeSkill()
        {
            int temp = 0;
            //bruh change when the skill tree is implemented
            if (skillTree.AllPossibleSkillTree.Count != skillTree.AllPossibleSkillTree.IndexOf(ability)+1)
            {
                temp = skillTree.AllPossibleSkillTree.IndexOf(ability) + 1;
                //Destroy(ability.gameObject);
                //PhotonNetwork.Destroy(ability.gameObject);
                
                //
                Destroy(ability.gameObject);
                ability = Instantiate(skillTree.AllPossibleSkillTree[temp],transform);
                //

                /*
                string loadResourceAbility = "Abilities/" + skillTree.AllPossibleSkillTree[temp].gameObject.name;
                GameObject abilityObject = PhotonNetwork.Instantiate(loadResourceAbility,transform.position,Quaternion.identity);
                ability = abilityObject.GetComponent<Ability>();
                */
            }
             
        }

        protected virtual void ApplyModifiers()
        {
            ability.Cooldown = cooldown;
        }
        
        
        
        protected void Cast()
        {
            ability.Cast();
        }
        
        
    }
}
