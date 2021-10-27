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
        [SerializeField] private int cost;
        [SerializeField] private Rarity rarity;
        [SerializeField] private Sprite icon;
        [SerializeField] private VideoClip demo;

        public int CardID => cardID;
        public int Cost => cost;
        public Rarity Rarity => rarity;
        public Sprite Icon => icon;
        public VideoClip Demo => demo;

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
