using System.Collections.Generic;
using _Project.Deck_Cards.Decks.Scripts;
using UnityEngine;

namespace _Project.Deck_Cards.DeckBuilder.Scripts
{
    public class DeckBuilder
    {
        public static DeckBuilder Instance => instance ??= new DeckBuilder();
        private static DeckBuilder instance;

        #region Privae Fields

        #endregion

        #region Public Properties

        public Deck CurrentSelection { get; private set; }

        #endregion

        #region Public Methods

        public void CreateDeck(string deckName)
        {
            if (string.IsNullOrEmpty(deckName))
            {
                var decks = DeckManager.Scripts.DeckManager.Instance.Decks;
                
                const string newDeckName = "newDeck";
                
                //Set deckName to generic name
                deckName = newDeckName;
                
                if (decks.Count > 0)
                {
                    var deckNameIndex = 0;
                    var index = decks.FindIndex(deck => deck.DeckName == deckName);
                    // if generic Name is already taken search increase Number at end until deckName is not taken
                    while (index != -1 && deckNameIndex < 100)
                    {
                        Debug.Log($"DeckIndex {index}");
                        deckNameIndex++;
                        deckName = newDeckName + deckNameIndex;
                        index = decks.FindIndex(deck => deck.DeckName == deckName);
                    }
                }
            }

            CurrentSelection = ScriptableObject.CreateInstance<Deck>();
            CurrentSelection.DeckName = deckName;
        }

        /// <summary>
        /// Clears current selection
        /// </summary>
        /// <returns>CurrentSelection</returns>
        public Deck Save()
        {
            var deck = CurrentSelection;
            CurrentSelection = null;

            return deck;
        }

        #endregion
    }
}
