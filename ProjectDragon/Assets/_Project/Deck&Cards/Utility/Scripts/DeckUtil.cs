using System.Collections.Generic;
using System.Linq;
using Deck_Cards.Decks.Scripts;
using Faction;

namespace Deck_Cards.Utility.Scripts
{
    public class DeckUtil
    {
        #region Search

        public static List<Deck> SearchDecksByName(string cardName, List<Deck> decks)
        {
            var matches = new List<Deck>();
            decks.ForEach(deck =>
            {
                var startsWith = deck.DeckName.StartsWith(cardName);
                if (startsWith)
                    matches.Add(deck);
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

        #endregion

        #region Filter

        public static List<Deck> FilterByFaction(ClassAndFaction.Faction faction, List<Deck> decks)
        {
            var matches = new List<Deck>();
            decks.ForEach(deck =>
            {
                if (deck.Faction == faction)
                {
                    matches.Add(deck);
                }
            });
            return matches;
        }

        #endregion

        #region Sort

        public static List<Deck> SortByFaction(IEnumerable<Deck> decks)
        {
            var matches = decks.OrderBy(deck => deck.Faction).ToList();
            return matches;
        }

        public static List<Deck> SortCommandersByName(IEnumerable<Deck> decks)
        {
            var matches = decks.OrderBy(deck => deck.DeckName).ToList();
            return matches;
        }

        #endregion
    }
}