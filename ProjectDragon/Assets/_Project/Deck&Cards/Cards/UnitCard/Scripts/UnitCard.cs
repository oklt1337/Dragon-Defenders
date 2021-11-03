using Abilities.AbilityDataBase.Scripts;
using Deck_Cards.Cards.BaseCards.Scripts;
using Faction;
using SkillSystem.SkillTree.Scripts;
using UnityEngine;
using UnityEngine.Video;

namespace Deck_Cards.Cards.UnitCard.Scripts
{
    public abstract class UnitCard : BaseCard
    {
        [SerializeField] private int limit;
        [SerializeField] private int goldCost;

        public int GoldCost => goldCost;
        public int Limit => limit;
        
        public void Save(int cId, string cName, string cDescription, int cCost, GameObject cModel, Rarity cRarity,
            ClassAndFaction.Faction cFaction, ClassAndFaction.Class cClass, SkillTreeObj cSkillTreeObj, AbilityDataBase cAbilityDataBase, VideoClip cDemo,
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
            skillTreeObj = cSkillTreeObj;
            abilityDataBase = cAbilityDataBase;
            demo = cDemo;
            icon = cIcon;
            
            goldCost = cGoldCost;
            limit = cLimit;
        }
    }
}
