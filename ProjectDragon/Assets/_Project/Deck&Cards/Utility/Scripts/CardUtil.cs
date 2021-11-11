using System.Collections.Generic;
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

        public static List<BaseCard> FilterCommandersByFaction(ClassAndFaction.Faction faction, List<BaseCard> cards)
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
        
        public static List<BaseCard> FilterCommandersByClass(ClassAndFaction.Class cClass, List<BaseCard> cards)
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
        
        public static List<BaseCard> FilterCommandersByRarity(List<Rarity> rarities, List<BaseCard> cards)
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

        #endregion
    }
}