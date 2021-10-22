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

        [SerializeField] private List<Deck> decks = new List<Deck>();
        private Deck currentDeck;

        #endregion

        #region Private Fields

        private string currentDeckPath;

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
                    int deckNameIndex = 0;
                    int index = decks.FindIndex(deck => deck.DeckName == deckName);
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
            currentDeckPath = $"Assets/_Project/RuntimeAssets/{deckName}.asset";
        }

        /// <summary>
        /// Removes a Deck from deckList.
        /// </summary>
        /// <param name="deckName">string</param>
        /// <returns>bool = true if deckName could be found in deckList.</returns>
        public bool DeleteDeck(string deckName)
        {
            int index = decks.FindIndex(deck => deck.DeckName == deckName);

            if (index == -1)
                return false;
            
            decks.RemoveAt(index);
            return true;
        }

        public void SaveDeck()
        {
            decks.Add(currentDeck);

            AssetDatabase.CreateAsset(currentDeck, currentDeckPath);
            AssetDatabase.SaveAssets();

            currentDeck = null;
            currentDeckPath = string.Empty;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Deletes the Asset at last saved asset path.
        /// </summary>
        private void DeleteDeckAsset()
        {
            AssetDatabase.DeleteAsset(currentDeckPath);
        }

        #endregion
    }
}
