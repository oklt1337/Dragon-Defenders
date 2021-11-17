using System;
using System.IO;
using Deck_Cards.Decks.Scripts;
using Network.NetworkManager.Scripts;
using Newtonsoft.Json;
using UnityEngine;

namespace Deck_Cards.DeckBuilder.DeckSerialization.Scripts
{
    public static class DeckSerializer
    {
        private const string ProjectName = "Project-Dragon";
        private static readonly string ID = NetworkManager.Instance.PlayFabManager.PlayFabProfileHandler.PlayerProfile
            .ProfileModel.PlayerId;

        public static void SaveDeck(Deck deck)
        {
            var deckFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                ProjectName, ID);
            CreateDeckFolderAndUserFolder(deckFolderPath);
            
            File.WriteAllText(Path.Combine(deckFolderPath, deck.DeckName + ".json"),
                JsonConvert.SerializeObject(deck, Formatting.Indented));
        }

        public static void DeleteDeckSaveFile(Deck deck)
        {
            var deckFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                ProjectName, ID);

            var path = Path.Combine(deckFolderPath, deck.DeckName + ".json");
            Debug.Log(path);
            File.Delete(path);
        }

        private static void CreateDeckFolderAndUserFolder(string path)
        {
            if (Directory.Exists(path))
                return;

            Directory.CreateDirectory(path);
        }
    }
}