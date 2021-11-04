using System.Collections.Generic;
using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
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
        private Client client;

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

        #endregion

        #region Private Methods

        protected virtual void Initialize(UnitCard unitCard)
        {
            //Base Implement
            card = unitCard;
            faction = unitCard.Faction;
            unitClass = unitCard.Class;
            ability = unitCard.abilityDataBase.Abilities[0].CreateInstance();
            client.Visitors.Add(ability);
            skillTree = unitCard.SkillTreeObj.CreateInstance(client);
        }

        public void Cast()
        {
            ability.AbilityObj.Cast(null, null);
        }

        #endregion
    }
}
