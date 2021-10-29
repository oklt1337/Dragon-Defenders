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
    public abstract class BaseCards : ScriptableObject
    {
        [SerializeField] private int cardID;
        [SerializeField] private string cardName;
        [SerializeField] private string description;
        [SerializeField] private int cost;
        [SerializeField] private Rarity rarity;
        [SerializeField] private VideoClip demo;
        [SerializeField] private Sprite icon;

        public int CardID => cardID;
        public string CardName => cardName;
        public string Description => description;
        public int Cost => cost;
        public Rarity Rarity => rarity;
        public VideoClip Demo => demo;
        public Sprite Icon => icon;

        public virtual void Save(int id, int cardCost, Rarity cardRarity, Sprite sprite, VideoClip clip)
        {
            cardID = id;
            cost = cardCost;
            rarity = cardRarity;
            icon = sprite;
            demo = clip;
        }
    }
}
