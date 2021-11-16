using System.Collections.Generic;
using System.IO;
using System.Linq;
using Deck_Cards.Decks.Scripts;
using Network.NetworkManager.Scripts;
using Photon.Pun;
using UnityEditor;
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

        // ReSharper disable once PossiblyMistakenUseOfParamsMethod
        public readonly string Root = string.Concat($"Assets/Resources/");

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

            LoadDecksFromResources();
        }

        #endregion

        #region Private Methods

        private void LoadDecksFromResources()
        {
         //   var id = NetworkManager.Instance.PlayFabManager.PlayFabProfileHandler.PlayerProfile.ProfileModel
         //       .PlayerId;
         //   var origin = string.Concat(deckBuilder.DeckRoot, "/", id);
         //   
         //   //Check if PlayerFolder Exist
         //   if (!AssetDatabase.IsValidFolder(origin))
         //       return;
         //   var paths = AssetDatabase.GetSubFolders(origin).ToList();
         //   var index = 0;
         //   //Load Every Deck from folder.
         //   foreach (var assetPath in paths
         //       .Select(path => string.Concat(path, "/", Path.GetFileName(Path.GetDirectoryName(path + "/")), "-Obj"))
         //       .Select(assetPath => assetPath.Remove(0, Root.Length)))
         //   {
         //       var deck = Resources.Load<Deck>(assetPath);
         //       deck.DeckId = index;
         //       if (deck == null) 
         //           continue;
         //       
         //       decks.Add(deck);
         //       index++;
           // }
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
          //  var index = decks.FindIndex(deck => deck.DeckId == deckId);
//
          //  if (index == -1)
          //      return false;
//
          //  var id = NetworkManager.Instance.PlayFabManager.PlayFabProfileHandler.PlayerProfile.ProfileModel
          //      .PlayerId;
          //  var path = string.Concat("Assets/Resources/Decks/", id, "/", decks[index].DeckName);
          //  var success = AssetDatabase.DeleteAsset(path);
          //  if (success)
          //  {
          //      decks.RemoveAt(index);
          //      
          //      if (decks.Count == 0)
          //      {
          //          AssetDatabase.DeleteAsset(
          //              string.Concat("Assets/Resources/Decks/", id));
          //      }
          //      else
          //      {
          //          for (var i = 0; i < decks.Count; i++)
          //          {
          //              decks[i].DeckId = i;
          //          }
          //      }
          //  }
          //  else
          //  {
          //      return false;
          //  }
//
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

        public void SetDeckAsDefault(int id)
        {
            var hashTable = PhotonNetwork.LocalPlayer.CustomProperties;
            if (hashTable.ContainsKey("PlayDeck"))
            {
                hashTable["PlayDeck"] = id;
            }
            else
            {
                hashTable.Add("PlayDeck", id);
            }
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashTable);
        }

        //ToDo: Deck on edit edit 2nd instance on save override main instance and have bool for isSaved.

        #endregion
    }
}