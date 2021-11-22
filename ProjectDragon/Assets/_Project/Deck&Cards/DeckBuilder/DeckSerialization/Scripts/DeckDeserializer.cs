using System;
using System.Collections.Generic;
using System.IO;
using Deck_Cards.Decks.Scripts;
using Network.NetworkManager.Scripts;
using Newtonsoft.Json;
using UnityEngine;

namespace Deck_Cards.DeckBuilder.DeckSerialization.Scripts
{
    public static class DeckDeserializer
    {
        private static readonly string ID = NetworkManager.Instance.PlayFabManager.PlayFabProfileHandler.PlayerProfile
            .ProfileModel.PlayerId;
        
        public static List<Deck> LoadDecks()
        {
            var deckFolder = Path.Combine(Application.persistentDataPath, ID);
            if (!Directory.Exists(deckFolder) || Directory.GetFiles(deckFolder).Length == 0)
                return new List<Deck>();

            var decks = new List<Deck>();
            var paths = Directory.GetFiles(deckFolder);
            for (var i = 0; i < paths.Length; i++)
            {
                var deck = LoadDeck(paths[i]);
                deck.DeckId = i;
                decks.Add(deck);
            }

            return decks;
        }

        private static Deck LoadDeck(string path)
        {
            return JsonUtility.FromJson<Deck>(File.ReadAllText(path));
        }
    }
}