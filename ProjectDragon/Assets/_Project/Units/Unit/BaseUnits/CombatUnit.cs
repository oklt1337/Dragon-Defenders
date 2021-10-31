using _Project.Deck_Cards.Cards.UnitCard.Scripts;

namespace _Project.Units.Unit.BaseUnits
{
    public class CombatUnit : Unit
    {
        private float attackDamageModifier;
        private float attackRange;
        
        protected override void Initialize(UnitCard unitCard)
        {
            var combatUnitCard = (CombatUnitCard) unitCard;
            base.Initialize(combatUnitCard);

            attackDamageModifier = combatUnitCard.AttackDamageModifier;
            attackRange = combatUnitCard.AttackRange;
        }
    }
}
