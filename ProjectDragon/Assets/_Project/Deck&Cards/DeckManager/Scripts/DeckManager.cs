using System.Collections.Generic;
using _Project.Deck_Cards.Decks.Scripts;
using PlayFab.ClientModels;
using UnityEditor;
using UnityEngine;

namespace _Project.Deck_Cards.DeckManager.Scripts
{
    public class DeckManager : MonoBehaviour
    {
        public static DeckManager Instance;

        #region Private Fields

        [SerializeField] private List<Deck> decks = new List<Deck>();

        #endregion

        #region Private Region

        private readonly DeckBuilder.Scripts.DeckBuilder deckBuilder = DeckBuilder.Scripts.DeckBuilder.Instance;

        #endregion

        #region Public Properties

        public List<Deck> Decks => decks;

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }
            else
            {
                Instance = this;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a deck and saves it in deck list.
        /// </summary>
        /// <param name="deckName"></param>
        public void CreateDeck(string deckName)
        {
            deckBuilder.CreateDeck(deckName);
        }

        /// <summary>
        /// Removes a Deck from deckList.
        /// </summary>
        /// <param name="deckName">string</param>
        /// <returns>bool = true if deckName could be found in deckList.</returns>
        public bool DeleteDeck(string deckName)
        {
            var index = decks.FindIndex(deck => deck.DeckName == deckName);

            if (index == -1)
                return false;
            
            decks.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Will set deckName to new Given deckName.
        /// </summary>
        /// <param name="index">int deckIndex</param>
        /// <param name="newDeckName">string</param>
        /// <returns></returns>
        public bool EditDeckName(int index, string newDeckName)
        {
            if (decks[index] == null)
                return false;
            
            decks[index].DeckName = newDeckName;
            return true;
        }

        /// <summary>
        /// Will set deckName to new Given deckName.
        /// </summary>
        /// <param name="deck">Deck deck to edit</param>
        /// <param name="newDeckName">string</param>
        /// <returns></returns>
        public bool EditDeckName(Deck deck, string newDeckName)
        {
            if (!decks.Contains(deck))
                return false;

            var index = decks.FindIndex(d => d == deck);
            if (index == -1) 
                return false;
            
            decks[index].DeckName = newDeckName;
            return true;
        }

        /// <summary>
        /// Saves a deck
        /// </summary>
        /// <param name="deck">deck to save</param>
        public void SaveDeck(Deck deck)
        {
            decks.Add(deck);
        }

        /// <summary>
        /// Saves Last Created Deck.
        /// </summary>
        public void SaveDeck()
        {
            decks.Add(deckBuilder.Save());
        }

        #endregion
    }
}
