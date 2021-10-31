using _Project.Deck_Cards.Cards.UnitCard.Scripts;

namespace _Project.Units.Unit.BaseUnits
{
    public class UtilityUnit : Unit
    {
        private float effectRange;

        protected override void Initialize(UnitCard unitCard)
        {
            var utilityUnitCard = (UtilityUnitCard) unitCard;
            base.Initialize(utilityUnitCard);

            effectRange = utilityUnitCard.EffectRange;
        }
    }
}
