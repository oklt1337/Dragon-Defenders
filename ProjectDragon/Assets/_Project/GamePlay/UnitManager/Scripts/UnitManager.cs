using System.Collections.Generic;
using Deck_Cards.Cards.UnitCard.Scripts;
using GamePlay.GameManager.Scripts;
using GamePlay.Player.PlayerModel.Scripts;
using Sirenix.Serialization;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace GamePlay.UnitManager.Scripts
{
    public class UnitManager : MonoBehaviour
    {
        [OdinSerialize] private readonly Dictionary<UnitCard, int> placedUnits = new Dictionary<UnitCard, int>();
        public List<Unit> Units { get; } = new List<Unit>();

        #region Unity Methods

        private void Awake()
        {
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += ChangeRay;
        }

        #endregion

        #region Private Methods

        private void ChangeRay(GameState state)
        {
            if (state == GameState.Build)    
            {
                foreach (var unit in Units)
                {
                    unit.gameObject.layer = 0;
                }
            }
            else
            {
                foreach (var unit in Units)
                {
                    unit.gameObject.layer = 2;
                }
            }
        }

        #endregion
        
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