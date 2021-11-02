using _Project.Abilities.Ability.Scripts;
using _Project.Deck_Cards.Cards.UnitCard.Scripts;
using _Project.Faction;
using _Project.SkillSystem.SkillTree;
using _Project.SkillSystem.SkillTree.Scripts;
using UnityEngine;

namespace _Project.Units.Unit.BaseUnits
{
    public class Unit : MonoBehaviour
    {
        #region Public Const Field

        public const string BasePath = "Cards/UnitCards/";

        #endregion
        
        #region SerializeFields

        [SerializeField] private Animator animator;
        
        #endregion

        #region Private Fields

        private string unitName;
        private string description;
        private int goldCost;
        private int limit;
        private GameObject unitModel;
        private ClassAndFaction.Faction faction;
        private ClassAndFaction.Class unitClass;
        private SkillTree skillTree;
        private Ability ability;
        
        //Runtime
        private byte level;
        private float experience;
        private float cooldown;
        
        private float coolDownModifier;
        private string currentSkillString;

        #endregion

        #region Public Propeties

        public Animator Animator
        {
            get => animator;
            set => animator = value;
        }

        public string UnitName
        {
            get => unitName;
            set => unitName = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public int GoldCost
        {
            get => goldCost;
            set => goldCost = value;
        }

        public int Limit
        {
            get => limit;
            set => limit = value;
        }

        public GameObject UnitModel
        {
            get => unitModel;
            set => unitModel = value;
        }

        public ClassAndFaction.Faction Faction
        {
            get => faction;
            set => faction = value;
        }

        public ClassAndFaction.Class UnitClass
        {
            get => unitClass;
            set => unitClass = value;
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

        public float Cooldown
        {
            get => cooldown;
            set => cooldown = value;
        }

        public float CoolDownModifier
        {
            get => coolDownModifier;
            set => coolDownModifier = value;
        }

        public string CurrentSkillString
        {
            get => currentSkillString;
            set => currentSkillString = value;
        }

        #endregion

        #region Private Methods

        protected virtual void Initialize(UnitCard unitCard)
        {
            //Base Implement
            unitName = unitCard.CardName;
            description = unitCard.Description;
            unitModel = unitCard.Model;
            faction = unitCard.Faction;
            unitClass = unitCard.Class;
            skillTree = unitCard.SkillTree;
            goldCost = unitCard.GoldCost;
            limit = unitCard.Limit;
        }

        public void Cast()
        {
            ability.Cast();
        }

        #endregion
    }
}
