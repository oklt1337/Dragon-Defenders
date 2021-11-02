using _Project.Deck_Cards.Decks.Scripts;
using UnityEditor;
using UnityEngine;

namespace _Project.Deck_Cards.DeckBuilder.Scripts
{
    public class DeckBuilder
    {
        public static DeckBuilder Instance => instance ??= new DeckBuilder();
        private static DeckBuilder instance;

        #region Public Properties

        public Deck CurrentSelection { get; private set; }

        #endregion

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

            CurrentSelection = ScriptableObject.CreateInstance<Deck>();
            CurrentSelection.DeckName = deckName;

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
            
            var guid = AssetDatabase.CreateFolder("Assets/Resources/Decks", deck.DeckName);
            var path = string.Concat(AssetDatabase.GUIDToAssetPath(guid), "/", deck.DeckName);
            AssetDatabase.CreateAsset(deck, string.Concat(path, "-Obj", ".asset"));
            AssetDatabase.SaveAssets();

            return deck;
        }

        #endregion
    }
}