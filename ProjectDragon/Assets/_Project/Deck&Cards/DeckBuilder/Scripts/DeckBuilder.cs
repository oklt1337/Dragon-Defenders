using Deck_Cards.DeckBuilder.DeckSerialization.Scripts;
using Deck_Cards.Decks.Scripts;
using Network.NetworkManager.Scripts;
using UnityEditor;
using UnityEngine;

namespace Deck_Cards.DeckBuilder.Scripts
{
    public class DeckBuilder
    {
        public static DeckBuilder Instance => instance ??= new DeckBuilder();
        private static DeckBuilder instance;

        private Deck CurrentSelection { get; set; }

        #region Public Methods

        public bool CreateDeck(string deckName)
        {
            var decks = DeckManager.Scripts.DeckManager.Instance.Decks;
            const string newDeckName = "newDeck";

            if (string.IsNullOrEmpty(deckName))
            {
                //Set deckName to generic name
                deckName = newDeckName;
            }

            var tryDeckName = deckName;
            if (decks.Count > 0)
            {
                var deckNameIndex = 0;
                var index = decks.FindIndex(deck => deck.DeckName == deckName);
                // if generic Name is already taken search increase Number at end until deckName is not taken
                while (index != -1 && deckNameIndex < 100)
                {
                    deckNameIndex++;
                    deckName = string.Concat(tryDeckName, deckNameIndex);
                    index = decks.FindIndex(deck => deck.DeckName == deckName);
                }

                if (deckNameIndex > 100)
                {
                    return false;
                }
            }

            CurrentSelection = new Deck
            {
                DeckName = deckName
            };

            return true;
        }

        /// <summary>
        /// Clears current selection
        /// </summary>
        /// <returns>CurrentSelection</returns>
        public Deck Save()
        {
            var deck = CurrentSelection;
            CurrentSelection = null;
            DeckSerializer.SaveDeck(deck);
            return deck;
        }

        public static void Save(Deck deck)
        {
            DeckSerializer.SaveDeck(deck);
        }

        #endregion
    }
}