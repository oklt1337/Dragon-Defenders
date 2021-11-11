using System.Collections.Generic;
using Deck_Cards.Cards.CommanderCard.Scripts;
using Deck_Cards.Cards.UnitCard.Scripts;

namespace Deck_Cards.Utility.Scripts
{
    public class CardUtil
    {
        public static List<UnitCard> SearchUnitsByName(string cardName, List<UnitCard> unitCards)
        {
            var matches = new List<UnitCard>();
            unitCards.ForEach(card =>
            {
                var startsWith = card.cardName.StartsWith(cardName);
                if (startsWith)
                    matches.Add(card);
            });
            
            if (matches.Count != 0) 
                return matches;
            {
                var index = unitCards.FindIndex(card => card.CardName == cardName);
                if (index != -1) 
                    matches.Add(unitCards[index]);
            }
            return matches;
        }
        
        public static List<CommanderCard> SearchCommandersByName(string cardName, List<CommanderCard> commanderCards)
        {
            var matches = new List<CommanderCard>();
            commanderCards.ForEach(card =>
            {
                var startsWith = card.cardName.StartsWith(cardName);
                if (startsWith)
                    matches.Add(card);
            });

            if (matches.Count != 0) 
                return matches;
            {
                var index = commanderCards.FindIndex(card => card.CardName == cardName);
                if (index != -1)
                    matches.Add(commanderCards[index]);
            }
            return matches;
        }
    }
}