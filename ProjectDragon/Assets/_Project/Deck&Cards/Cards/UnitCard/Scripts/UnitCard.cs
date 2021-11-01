using _Project.Abilities.Ability.CommanderAbilityDataBase.Scripts;
using _Project.Deck_Cards.Cards.BaseCards.Scripts;
using _Project.Faction;
using _Project.SkillSystem.SkillTree;
using UnityEngine;
using UnityEngine.Video;

namespace _Project.Deck_Cards.Cards.UnitCard.Scripts
{
    public abstract class UnitCard : BaseCard
    {
        [SerializeField] private int limit;
        [SerializeField] private int goldCost;
        [SerializeField] private UnitAbilityDataBase unitAbilityDataBase;

        public int GoldCost => goldCost;
        public int Limit => limit;

        public UnitAbilityDataBase UnitAbilityDataBase
        {
            get => unitAbilityDataBase;
            set => unitAbilityDataBase = value;
        }
        
        public void Save(int cId, string cName, string cDescription, int cCost, GameObject cModel, Rarity cRarity,
            ClassAndFaction.Faction cFaction, ClassAndFaction.Class cClass, SkillTree cSkillTree, VideoClip cDemo,
            Sprite cIcon, int cGoldCost, int cLimit, UnitAbilityDataBase cUnitAbilityDataBase)
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
            goldCost = cGoldCost;
            limit = cLimit;
            unitAbilityDataBase = cUnitAbilityDataBase;
        }
    }
}
