using Abilities.AbilityDataBase.Scripts;
using Faction;
using SkillSystem.SkillTree.Scripts;
using UnityEngine;
using UnityEngine.Video;

namespace Deck_Cards.Cards.BaseCards.Scripts
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

        [Header("Specifications")] 
        [SerializeField] internal GameObject model;
        [SerializeField] internal Rarity rarity;
        [SerializeField] internal ClassAndFaction.Faction faction;
        [SerializeField] internal ClassAndFaction.Class @class;
        [SerializeField] internal SkillTreeObj skillTreeObj;
        [SerializeField] internal AbilityDataBase abilityDataBase;

        [Header("Visuals")]
        [SerializeField] internal VideoClip demo;
        [SerializeField] internal Sprite icon;

        public int CardID => cardID;

        public string CardName
        {
            get => cardName;
            internal set => cardName = value;
        }

        public string Description => description;

        public GameObject Model => model;

        public Rarity Rarity => rarity;

        public ClassAndFaction.Faction Faction => faction;

        public ClassAndFaction.Class Class => @class;

        public SkillTreeObj SkillTreeObj
        {
            get => skillTreeObj;
            internal set => skillTreeObj = value;
        }

        public AbilityDataBase AbilityDataBase
        {
            get => abilityDataBase;
            internal set => abilityDataBase = value;
        }

        public VideoClip Demo => demo;

        public Sprite Icon => icon;
    }

}