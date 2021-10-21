using UnityEngine;

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
        [SerializeField] private Sprite displayImage;
        [SerializeField] private Rarity rarity;

        public Sprite DisplayImage => displayImage;
        public Rarity Rarity => rarity;
    }
}
