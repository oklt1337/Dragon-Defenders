using System.Collections.Generic;
using _Project.Abilities.Ability.Scripts;
using _Project.Abilities.AbilityDataBase;
using _Project.Abilities.AbilityDataBase.Scripts;
using _Project.Deck_Cards.Cards.BaseCards.Scripts;
using _Project.Faction;
using _Project.SkillSystem.SkillTree.Scripts;
using UnityEngine;
using UnityEngine.Video;

namespace _Project.Deck_Cards.Cards.CommanderCard.Scripts
{
    public class CommanderCard : BaseCard
    {
        [SerializeField] private float health;
        [SerializeField] private float mana;
        [SerializeField] private float attackDamageModifier;
        [SerializeField] private float defense;
        [SerializeField] private float speed;

        public float Health => health;

        public float Mana => mana;

        public float AttackDamageModifier => attackDamageModifier;

        public float Defense => defense;

        public float Speed => speed;

        public void Save(int cId, string cName, string cDescription, int cCost, GameObject cModel, Rarity cRarity,
            ClassAndFaction.Faction cFaction, ClassAndFaction.Class cClass, SkillTree cSkillTree,
            AbilityDataBase cAbilityDataBase, VideoClip cDemo,
            Sprite cIcon, float cHealth, float cMana, float cAttackDamageModifier, float cDefense, float cSpeed)
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
            abilityDataBase = cAbilityDataBase;
            demo = cDemo;
            icon = cIcon;

            health = cHealth;
            mana = cMana;
            attackDamageModifier = cAttackDamageModifier;
            defense = cDefense;
            speed = cSpeed;
        }
    }
}