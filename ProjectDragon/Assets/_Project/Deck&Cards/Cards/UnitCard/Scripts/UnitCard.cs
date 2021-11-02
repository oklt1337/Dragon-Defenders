using System.Collections.Generic;
using _Project.Abilities.Ability.Scripts;
using _Project.Abilities.AbilityDataBase;
using _Project.Abilities.AbilityDataBase.Scripts;
using _Project.Deck_Cards.Cards.BaseCards.Scripts;
using _Project.Faction;
using _Project.SkillSystem.SkillTree.Scripts;
using UnityEngine;
using UnityEngine.Video;

namespace _Project.Deck_Cards.Cards.UnitCard.Scripts
{
    public abstract class UnitCard : BaseCard
    {
        [SerializeField] private int limit;
        [SerializeField] private int goldCost;

        public int GoldCost => goldCost;
        public int Limit => limit;
        
        public void Save(int cId, string cName, string cDescription, int cCost, GameObject cModel, Rarity cRarity,
            ClassAndFaction.Faction cFaction, ClassAndFaction.Class cClass, SkillTree cSkillTree, AbilityDataBase cAbilityDataBase, VideoClip cDemo,
            Sprite cIcon, int cGoldCost, int cLimit)
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
            
            goldCost = cGoldCost;
            limit = cLimit;
        }
    }
}
