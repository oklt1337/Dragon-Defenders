using _Project.Units.Unit.BaseUnits;
using UnityEngine;

namespace _Project.Deck_Cards.Cards.UnitCard.Scripts
{
    [CreateAssetMenu(menuName = "Tool/Cards/UnitCard", fileName = "UnitCard")]
    public class UnitCard : BaseCards.Scripts.BaseCards
    {
        [SerializeField] private Unit unit;

        public Unit Unit => unit;
    }
}
