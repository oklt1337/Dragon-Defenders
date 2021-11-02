using _Project.Abilities.AbilityDataBase;
using _Project.Abilities.AbilityDataBase.Scripts;
using _Project.Faction;
using _Project.SkillSystem.SkillTree;
using _Project.SkillSystem.SkillTree.Scripts;
using UnityEngine;
using UnityEngine.Video;

namespace _Project.Deck_Cards.Cards.BaseCards.Scripts
{
    public enum Rarity
    {
        Common,
        Rare,
        Epic,
        Legendary,
        Mythical
    }

    public abstract class BaseCard : ScriptableObject
    {
        [Header("General")] 
        [SerializeField] internal int cardID;
        [SerializeField] internal string cardName;
        [SerializeField] internal string description;
        [SerializeField] internal int cost;

        [Header("Specifications")] 
        [SerializeField] internal GameObject model;
        [SerializeField] internal Rarity rarity;
        [SerializeField] internal ClassAndFaction.Faction faction;
        [SerializeField] internal ClassAndFaction.Class @class;
        [SerializeField] internal SkillTree skillTree;
        [SerializeField] internal AbilityDataBase abilityDataBase;

        [Header("Visuals")]
        [SerializeField] internal VideoClip demo;
        [SerializeField] internal Sprite icon;

        public int CardID => cardID;

        public string CardName
        {
            get => cardName;
            set => cardName = value;
        }

        public string Description => description;

        public int Cost => cost;

        public GameObject Model => model;

        public Rarity Rarity => rarity;

        public ClassAndFaction.Faction Faction => faction;

        public ClassAndFaction.Class Class => @class;

        public SkillTree SkillTree
        {
            get => skillTree;
            set => skillTree = value;
        }

        public AbilityDataBase AbilityDataBase
        {
            get => abilityDataBase;
            set => abilityDataBase = value;
        }

        public VideoClip Demo => demo;

        public Sprite Icon => icon;
    }

}