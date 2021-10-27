using System.Collections.Generic;
using _Project.Deck_Cards.Cards.BaseCards.Scripts;
using _Project.Deck_Cards.Cards.CommanderCard.Scripts;
using _Project.Deck_Cards.Cards.UnitCard.Scripts;
using _Project.Faction;
using UnityEngine;

namespace _Project.Deck_Cards.Decks.Scripts
{
    [CreateAssetMenu(menuName = "Tool/Cards/Deck", fileName = "Deck")]
    public class Deck : ScriptableObject
    {
        #region Serialze Fields
        
        [SerializeField] private int deckId;
        [SerializeField] private string deckName;
        [SerializeField] private ClassAndFaction.Faction faction;
        [SerializeField] private CommanderCard commanderCard;
        [SerializeField] private List<UnitCard> unitCards = new List<UnitCard>();

        #endregion

        #region Private Fields

        private const int MaxCards = 8;

        #endregion
        
        #region Public Properties

        public int DeckId => deckId;

        public string DeckName
        {
            get => deckName;
            set => deckName = value;
        }

        public bool IsUseAble
        {
            get
            {
                if (unitCards.Count == MaxCards)
                {
                    return true;
                }

                return commanderCard != null;
            }
        }

        public ClassAndFaction.Faction Faction
        {
            get => faction;
            set => faction = value;
        }

        public CommanderCard CommanderCard => commanderCard;

        public List<UnitCard> UnitCards => unitCards;

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a card to the deck.
        /// Card can be a commander or a unit.
        /// </summary>
        /// <param name="card">BaseCards</param>
        /// <returns>bool = true if card could be added.</returns>
        public bool AddCard(BaseCards card)
        {
            if (card.GetType() == typeof(CommanderCard))
            {
                if (commanderCard != null || ((CommanderCard) card).Commander.faction != faction)
                    return false;
                
                commanderCard = (CommanderCard) card;
                return true;
            }

            if (card.GetType() != typeof(UnitCard)) 
                return false;
            
            if (unitCards.Contains((UnitCard) card))
                return false;
            
            if (unitCards.Count == MaxCards)
                return false;

            unitCards.Add((UnitCard) card);
            return true;
        }

        /// <summary>
        /// Removes a card from the deck.
        /// Card can be a commander or a unit.
        /// </summary>
        /// <param name="card">BaseCards</param>
        /// <returns>bool = true if card could be removed.</returns>
        public bool RemoveCard(BaseCards card)
        {
            if (card.GetType() == typeof(CommanderCard))
            {
                if (commanderCard == null)
                    return false;
                
                commanderCard = null;
                return true;
            }

            if (card.GetType() != typeof(UnitCard)) 
                return false;

            if (!unitCards.Contains((UnitCard)card))
                return false;
            
            unitCards.Remove((UnitCard) card);
            return true;
        }

        #endregion
    }
}
