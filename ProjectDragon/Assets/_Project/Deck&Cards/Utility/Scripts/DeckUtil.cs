using System.Collections.Generic;
using Deck_Cards.Decks.Scripts;

namespace Deck_Cards.Utility.Scripts
{
    public class DeckUtil
    {
        public static List<Deck> SearchDecksByName(string cardName, List<Deck> decks)
        {
            var matches = new List<Deck>();
            decks.ForEach(card =>
            {
                var startsWith = card.DeckName.StartsWith(cardName);
                if (startsWith)
                    matches.Add(card);
            });
            
            if (matches.Count != 0) 
                return matches;
            {
                var index = decks.FindIndex(card => card.DeckName == cardName);
                if (index != -1) 
                    matches.Add(decks[index]);
            }
            return matches;
        }
    }
}