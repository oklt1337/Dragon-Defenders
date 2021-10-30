using _Project.Abilities.Ability.CommanderAbilityDataBase.Scripts;
using _Project.Deck_Cards.Cards.BaseCards.Scripts;
using _Project.Faction;
using _Project.SkillSystem.SkillTree;
using UnityEngine;
using UnityEngine.Video;

namespace _Project.Deck_Cards.Cards.CommanderCard.Scripts
{
    public class CommanderCard : BaseCards.Scripts.BaseCard
    {
        [SerializeField] private float health;
        [SerializeField] private float mana;
        [SerializeField] private float attackDamageModifier;
        [SerializeField] private float defense;
        [SerializeField] private float speed;
        [SerializeField] private CommanderAbilityDataBase commanderAbilityDataBase;

        public float Health => health;

        public float Mana => mana;

        public float AttackDamageModifier => attackDamageModifier;

        public float Defense => defense;

        public float Speed => speed;

        public CommanderAbilityDataBase CommanderAbilityDataBase
        {
            get => commanderAbilityDataBase;
            set => commanderAbilityDataBase = value;
        }

        public void Save(int cId, string cName, string cDescription, int cCost, GameObject cModel, Rarity cRarity,
            ClassAndFaction.Faction cFaction, ClassAndFaction.Class cClass, SkillTree cSkillTree, VideoClip cDemo,
            Sprite cIcon, float cHealth, float cMana, float cAttackDamageModifier, float cDefense, float cSpeed, CommanderAbilityDataBase cCommanderAbilityDataBase)
        {
            cardID = cId;
            cardName = cName;
            description = cDescription;
            cost = cCost;
            model = cModel;
            rarity = cRarity;
            faction = cFaction;
            @class = cClass;
            skillTree = cSkillTree;
            demo = cDemo;
            icon = cIcon;
            health = cHealth;
            mana = cMana;
            attackDamageModifier = cAttackDamageModifier;
            defense = cDefense;
            speed = cSpeed;
            commanderAbilityDataBase = cCommanderAbilityDataBase;
        }
    }
}