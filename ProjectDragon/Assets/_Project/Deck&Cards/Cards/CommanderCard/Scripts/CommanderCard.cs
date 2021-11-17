using System;
using Abilities.AbilityDataBase.Scripts;
using Deck_Cards.Cards.BaseCards.Scripts;
using Faction;
using SkillSystem.SkillTree.Scripts;
using UnityEngine;
using UnityEngine.Video;

namespace Deck_Cards.Cards.CommanderCard.Scripts
{
    [Serializable]
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

        public void Save(int cId, string cName, string cDescription, GameObject cModel, Rarity cRarity,
            ClassAndFaction.Faction cFaction, ClassAndFaction.Class cClass, SkillTreeObj cSkillTreeObj,
            AbilityDataBase cAbilityDataBase, VideoClip cDemo,
            Sprite cIcon, float cHealth, float cMana, float cAttackDamageModifier, float cDefense, float cSpeed)
        {
            cardID = cId;
            cardName = cName;
            description = cDescription;
            model = cModel;
            rarity = cRarity;
            faction = cFaction;
            @class = cClass;
            skillTreeObj = cSkillTreeObj;
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