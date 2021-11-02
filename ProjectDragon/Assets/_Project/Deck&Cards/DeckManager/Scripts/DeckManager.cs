using System.Collections.Generic;
using _Project.Deck_Cards.Decks.Scripts;
using PlayFab.ClientModels;
using Sirenix.OdinInspector;
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
        public bool CreateDeck(string deckName)
        {
            var success = deckBuilder.CreateDeck(deckName);
            if (!success) 
                return false;
            
            var deck = deckBuilder.Save();
            deck.DeckId = decks.Count;
            decks.Add(deck);
            return true;
        }


        /// <summary>
        /// Removes a Deck from deckList.
        /// </summary>
        /// <param name="deckId">DeckId</param>
        /// <returns>bool = true if deckName could be found in deckList.</returns>
        [Button]
        public bool DeleteDeck(int deckId)
        {
            var index = decks.FindIndex(deck => deck.DeckId == deckId);

            if (index == -1)
                return false;

            var path = string.Concat("Assets/Resources/Decks/", decks[index].DeckName);
            var success = AssetDatabase.DeleteAsset(path);
            if (success)
            {
                decks.RemoveAt(index);
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Will set deckName to new Given deckName.
        /// </summary>
        /// <param name="index">int deckIndex</param>
        /// <param name="newDeckName">string</param>
        /// <returns>bool if edit worked</returns>
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
        /// <returns>bool if edit worked</returns>
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

        //ToDo: Deck on edit edit 2nd instance on save override main instance and have bool for isSaved.

        #endregion
    }
}
