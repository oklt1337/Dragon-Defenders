using System.Collections.Generic;
using System.Linq;
using Deck_Cards.Cards.BaseCards.Scripts;
using Deck_Cards.Cards.UnitCard.Scripts;
using Faction;

namespace Deck_Cards.Utility.Scripts
{
    public class CardUtil
    {
        #region Search

        public static List<BaseCard> SearchByName(string cardName, List<BaseCard> cards)
        {
            var matches = new List<BaseCard>();
            cards.ForEach(card =>
            {
                var startsWith = card.cardName.StartsWith(cardName);
                if (startsWith)
                    matches.Add(card);
            });
            
            if (matches.Count != 0) 
                return matches;
            {
                var index = cards.FindIndex(card => card.CardName == cardName);
                if (index != -1) 
                    matches.Add(cards[index]);
            }
            return matches;
        }

        #endregion

        #region Filter

        public static List<BaseCard> FilterByFaction(ClassAndFaction.Faction faction, List<BaseCard> cards)
        {
            var matches = new List<BaseCard>();
            cards.ForEach(card =>
            {
                if (card.Faction == faction)
                {
                    matches.Add(card);
                }
            });
            return matches;
        }
        
        public static List<BaseCard> FilterByClass(ClassAndFaction.Class cClass, List<BaseCard> cards)
        {
            var matches = new List<BaseCard>();
            cards.ForEach(card =>
            {
                if (card.Class == cClass)
                {
                    matches.Add(card);
                }
            });
            return matches;
        }
        
        public static List<BaseCard> FilterByRarity(List<Rarity> rarities, List<BaseCard> cards)
        {
            var matches = new List<BaseCard>();
            cards.ForEach(card =>
            {
                if (rarities.Contains(card.Rarity))
                {
                    matches.Add(card);
                }
            });
            return matches;
        }
        
        public static List<UnitCard> FilterUnitCardsByLimit(List<int> limits, List<UnitCard> cards)
        {
            var matches = new List<UnitCard>();
            cards.ForEach(card =>
            {
                if (limits.Contains(card.Limit))
                {
                    matches.Add(card);
                }
            });
            return matches;
        }

        #endregion

        #region Sort

        public static List<BaseCard> SortByFaction(IEnumerable<BaseCard> cards)
        {
            var matches = cards.OrderBy(card => card.faction).ToList();
            return matches;
        }
        
        public static List<BaseCard> SortByClass(IEnumerable<BaseCard> cards)
        {
            var matches = cards.OrderBy(card => card.Class).ToList();
            return matches;
        }
        
        public static List<BaseCard> SortByName(IEnumerable<BaseCard> cards)
        {
            var matches = cards.OrderBy(card => card.CardName).ToList();
            return matches;
        }
        
        public static List<BaseCard> SortByRarity(IEnumerable<BaseCard> cards)
        {
            var matches = cards.OrderBy(card => card.Rarity).ToList();
            return matches;
        }
        
        public static List<UnitCard> SortUnitCardsByLimit(IEnumerable<UnitCard> cards)
        {
            var matches = cards.OrderBy(card => card.Limit).ToList();
            return matches;
        }

        #endregion
    }
}