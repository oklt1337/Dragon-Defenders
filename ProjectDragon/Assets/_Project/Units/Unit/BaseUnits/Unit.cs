using Abilities.Ability.Scripts;
using Deck_Cards.Cards.BaseCards.Scripts;
using Deck_Cards.Cards.UnitCard.Scripts;
using Faction;
using SkillSystem.SkillTree.Scripts;
using UnityEngine;

namespace Units.Unit.BaseUnits
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

        private UnitCard card;
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

        public UnitCard Card => card;

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
            card = unitCard;
            faction = unitCard.Faction;
            unitClass = unitCard.Class;
            skillTree = unitCard.SkillTreeObj.CreateInstance();
            ability = unitCard.abilityDataBase.Abilities[0].CreateInstance();
        }

        public void Cast()
        {
            ability.AbilityObj.Cast(null, null);
        }

        #endregion
    }
}
