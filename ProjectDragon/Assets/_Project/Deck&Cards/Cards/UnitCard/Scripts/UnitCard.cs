using _Project.Abilities.Ability.CommanderAbilityDataBase.Scripts;
using UnityEngine;

namespace _Project.Deck_Cards.Cards.UnitCard.Scripts
{
    public enum UnitType
    {
        Combat,
        Utility
    }
    
    public abstract class UnitCard : BaseCards.Scripts.BaseCard
    {
        [SerializeField] internal UnitType unitType;
        [SerializeField] internal int goldCost;
        [SerializeField] internal UnitAbilityDataBase unitAbilityDataBase;

        public UnitType UnitType
        {
            get => unitType;
            set => unitType = value;
        }

        public int GoldCost => goldCost;

        public UnitAbilityDataBase UnitAbilityDataBase
        {
            get => unitAbilityDataBase;
            set => unitAbilityDataBase = value;
        }
    }
}
