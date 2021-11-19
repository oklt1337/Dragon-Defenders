using System.Collections.Generic;
using Deck_Cards.Cards.UnitCard.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace GamePlay.UnitManager.Scripts
{
    public class UnitManager : MonoBehaviour
    {
        public Dictionary<UnitCard, int> PlacedUnits { get; } = new Dictionary<UnitCard, int>();

        public List<Unit> Units { get; } = new List<Unit>();
        
        #region Public Methods
        
        public bool AddPlacedUnit(UnitCard unitCard)
        {
            if (!PlacedUnits.ContainsKey(unitCard)) 
                return false;
            if (PlacedUnits[unitCard] >= unitCard.Limit) 
                return false;
            PlacedUnits[unitCard]++;
            return true;
        }
        
        public bool RemovePlacedUnit(UnitCard unitCard)
        {
            if (!PlacedUnits.ContainsKey(unitCard)) 
                return false;
            if (PlacedUnits[unitCard] <= 0) 
                return false;
            PlacedUnits[unitCard]--;
            return true;
        }

        #endregion
    }
}