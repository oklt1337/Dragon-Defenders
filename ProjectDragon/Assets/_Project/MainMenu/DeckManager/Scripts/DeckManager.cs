using System;
using System.Collections.Generic;
using _Project.Deck_Cards.Decks.Scripts;
using UnityEditor;
using UnityEngine;

namespace _Project.MainMenu.DeckManager.Scripts
{
    public class DeckManager : MonoBehaviour
    {
        public static DeckManager Instance;

        #region Private Fields

        [SerializeField] private List<Deck> _decks = new List<Deck>();
        private Deck _currentDeck;

        #endregion

        #region Private Fields

        private string _currentDeckPath;

        #endregion

        #region Public Properties

        public List<Deck> Decks => _decks;
        public Deck CurrentDeck => _currentDeck;

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
                
                Debug.Log(_decks.Count);
                if (_decks.Count > 0)
                {
                    int deckNameIndex = 0;
                    int index = _decks.FindIndex(deck => deck.DeckName == deckName);
                    // if generic Name is already taken search increase Number at end until deckName is not taken
                    while (index != -1 && deckNameIndex < 100)
                    {
                        Debug.Log($"DeckIndex {index}");
                        deckNameIndex++;
                        deckName = newDeckName + deckNameIndex;
                        index = _decks.FindIndex(deck => deck.DeckName == deckName);
                    }
                }
            }
            
            _currentDeck = ScriptableObject.CreateInstance<Deck>();
            _currentDeck.DeckName = deckName;
            _currentDeckPath = $"Assets/_Project/RuntimeAssets/{deckName}.asset";
        }

        /// <summary>
        /// Removes a Deck from deckList.
        /// </summary>
        /// <param name="deckName">string</param>
        /// <returns>bool = true if deckName could be found in deckList.</returns>
        public bool DeleteDeck(string deckName)
        {
            int index = _decks.FindIndex(deck => deck.DeckName == deckName);

            if (index == -1)
                return false;
            
            _decks.RemoveAt(index);
            return true;
        }

        public void SaveDeck()
        {
            _decks.Add(_currentDeck);

            AssetDatabase.CreateAsset(_currentDeck, _currentDeckPath);
            AssetDatabase.SaveAssets();

            _currentDeck = null;
            _currentDeckPath = string.Empty;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Deletes the Asset at last saved asset path.
        /// </summary>
        private void DeleteDeckAsset()
        {
            AssetDatabase.DeleteAsset(_currentDeckPath);
        }

        #endregion
    }
}
