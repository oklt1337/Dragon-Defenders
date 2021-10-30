using _Project.Abilities.Ability.CommanderAbilityDataBase.Scripts;
using _Project.Deck_Cards.Cards.BaseCards.Scripts;
using _Project.Faction;
using _Project.SkillSystem.SkillTree;
using UnityEngine;
using UnityEngine.Video;

namespace _Project.Deck_Cards.Cards.UnitCard.Scripts
{
    public class UtilityUnitCard : UnitCard
    {
        [SerializeField] private float effectRange;

        public float EffectRange => effectRange;

        public void Save(int cId, string cName, string cDescription, int cCost, GameObject cModel, Rarity cRarity,
            ClassAndFaction.Faction cFaction, ClassAndFaction.Class cClass, SkillTree cSkillTree, VideoClip cDemo,
            Sprite cIcon, UnitType cUnitType, int cGoldCost, UnitAbilityDataBase cUnitAbilityDataBase, float cEffectRange)
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
            unitType = cUnitType;
            goldCost = cGoldCost;
            unitAbilityDataBase = cUnitAbilityDataBase;
            effectRange = cEffectRange;
        }
    }
}
