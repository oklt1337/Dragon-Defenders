using System.Collections.Generic;
using _Project.Deck_Cards.Decks.Scripts;
using UnityEditor;
using UnityEngine;

namespace _Project.Deck_Cards.DeckManager.Scripts
{
    public class DeckManager : MonoBehaviour
    {
        public static DeckManager Instance;

        #region Private Fields

        [SerializeField] private List<Deck> decks = new List<Deck>();
        private Deck currentDeck;

        #endregion

        #region Public Properties

        public List<Deck> Decks => decks;
        public Deck CurrentDeck => currentDeck;

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                CreateDeck(null);
            }
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveDeck();
            }
        }

        #endregion

        #region Public Methods
        
        public void CreateDeck(string deckName)
        {
            if (string.IsNullOrEmpty(deckName))
            {
                const string newDeckName = "newDeck";
                
                //Set deckName to generic name
                deckName = newDeckName;
                
                Debug.Log(decks.Count);
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

            currentDeck = ScriptableObject.CreateInstance<Deck>();
            currentDeck.DeckName = deckName;
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
        /// Saves the deck in the List.
        /// </summary>
        public void SaveDeck()
        {
            decks.Add(currentDeck);
            currentDeck = null;
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

        #endregion
    }
}
