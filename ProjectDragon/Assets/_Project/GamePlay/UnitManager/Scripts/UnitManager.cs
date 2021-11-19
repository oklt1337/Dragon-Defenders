using System.Collections.Generic;
using Deck_Cards.Cards.UnitCard.Scripts;
using Sirenix.Serialization;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace GamePlay.UnitManager.Scripts
{
    public class UnitManager : MonoBehaviour
    {
        [OdinSerialize] private readonly Dictionary<UnitCard, int> placedUnits = new Dictionary<UnitCard, int>();
        public List<Unit> Units { get; } = new List<Unit>();
        
        #region Public Methods
        
        public bool AddPlacedUnit(UnitCard unitCard)
        {
            if (!placedUnits.ContainsKey(unitCard))
            {
                placedUnits.Add(unitCard, 1);
                return true;
            }
            if (placedUnits[unitCard] >= unitCard.Limit) 
                return false;
            placedUnits[unitCard]++;
            return true;
        }
        
        public bool RemovePlacedUnit(UnitCard unitCard)
        {
            if (!placedUnits.ContainsKey(unitCard)) 
                return false;
            if (placedUnits[unitCard] <= 0) 
                return false;
            placedUnits[unitCard]--;
            return true;
        }

        #endregion
    }
}