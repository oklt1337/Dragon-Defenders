using _Project.Deck_Cards.Cards.BaseCards.Scripts;
using _Project.Units.Unit.BaseUnitDatabases;
using UnityEngine;
using UnityEngine.Video;

namespace _Project.Deck_Cards.Cards.UnitCard.Scripts
{
    [CreateAssetMenu(menuName = "Tool/Cards/UnitCard", fileName = "UnitCard")]
    public class UnitCard : BaseCards.Scripts.BaseCards
    {
        [SerializeField] private BaseUnitDataBase unit;
        private BaseCards.Scripts.BaseCards baseCardsImplementation;

        public BaseUnitDataBase Unit => unit;
        public override void Save(int id, int cost, Rarity rarity, Sprite sprite, VideoClip clip)
        {
            
        }
    }
}
