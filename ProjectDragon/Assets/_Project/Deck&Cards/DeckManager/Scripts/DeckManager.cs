using System.Collections.Generic;
using System.Linq;
using Deck_Cards.DeckBuilder.DeckSerialization.Scripts;
using Deck_Cards.Decks.Scripts;
using GamePlay.GameManager.Scripts;
using Photon.Pun;
using UnityEngine;

namespace Deck_Cards.DeckManager.Scripts
{
    public class DeckManager : MonoBehaviour
    {
        public static DeckManager Instance;

        #region Private Serializable Fields

        [SerializeField] private List<Deck> decks = new List<Deck>();

        #endregion

        #region Private Fields

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

            decks = DeckDeserializer.LoadDecks();
            
            //Setting Default Deck
            var index = decks.FindIndex(deck => deck.IsDefault);
            if (index != -1)
            {
                SetDeckAsDefault(index);
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
        public bool DeleteDeck(int deckId)
        {
            var index = Decks.FindIndex(deck => deck.DeckId == deckId);
            if (index == -1)
                return false;
            DeckSerializer.DeleteDeckSaveFile(Decks[index]);
            decks.Remove(decks[index]);
            for (var i = 0; i < Decks.Count; i++)
            {
                Decks[i].DeckId = i;
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
        public static void SaveDeck(Deck deck)
        {
            DeckBuilder.Scripts.DeckBuilder.Save(deck);
        }

        public void SetDeckAsDefault(int id)
        {
            foreach (var deck in Instance.Decks.Where(t => t.IsDefault))
            {
                deck.IsDefault = false;
                break;
            }

            Decks[id].IsDefault = true;
            SaveDeck(Decks[id]);
            GameManager.DefaultDeck = Decks[id];
        }

        //ToDo: Deck on edit edit 2nd instance on save override main instance and have bool for isSaved.

        #endregion
    }
}