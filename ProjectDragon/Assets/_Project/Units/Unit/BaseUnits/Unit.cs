using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Deck_Cards.Cards.UnitCard.Scripts;
using _Project.Faction;
using _Project.SkillSystem.SkillTree;
using UnityEngine;

namespace _Project.Units.Unit.BaseUnits
{
    public class Unit : MonoBehaviour
    {
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
        internal float cooldown;
        
        private float coolDownModifier;
        private string currentSkillString;

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
