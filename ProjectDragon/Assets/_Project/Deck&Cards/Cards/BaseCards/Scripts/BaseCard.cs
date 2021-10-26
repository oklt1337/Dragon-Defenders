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
        [SerializeField] private Sprite displayImage;
        [SerializeField] private Rarity rarity;
        [SerializeField] private VideoClip videoClip;

        public int CardID => cardID;
        public int Cost => cost;
        public Sprite DisplayImage => displayImage;
        public Rarity Rarity => rarity;
        public VideoClip VideoClip => videoClip;
    }
}
