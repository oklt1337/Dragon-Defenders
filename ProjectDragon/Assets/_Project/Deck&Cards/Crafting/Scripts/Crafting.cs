using System;
using Deck_Cards.Cards.BaseCards.Scripts;
using PlayerProfile.Profile.Scripts;
using Sirenix.Utilities;

namespace Deck_Cards.Crafting.Scripts
{
    public class Crafting
    {
        public static bool Disenchant(int cardID)
        {
            var success = DisenchantCommanderCard(Profile.Instance, cardID);
            if (!success)
            {
                success = DisenchantUnitCard(Profile.Instance, cardID);
            }
            return success;
        }

        private static bool DisenchantCommanderCard(Profile profile, int cardID)
        {
            var success = false;
            profile.CommanderCards.ForEach(pair =>
            {
                if (!pair.Key.CardID.Equals(cardID))
                    return;
                if (pair.Value <= 0)
                    return;
                if (pair.Key.Rarity == Rarity.Basic)
                    return;

                profile.CommanderCards[pair.Key]--;
                switch (pair.Key.Rarity)
                {
                    case Rarity.Basic:
                        break;
                    case Rarity.Common:
                        success = true;
                        profile.Dust += 50;
                        break;
                    case Rarity.Rare:
                        success = true;
                        profile.Dust += 125;
                        break;
                    case Rarity.Epic:
                        success = true;
                        profile.Dust += 250;
                        break;
                    case Rarity.Legendary:
                        success = true;
                        profile.Dust += 1000;
                        break;
                    case Rarity.Mythical:
                        success = true;
                        profile.Dust += 3000;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });

            return success;
        }

        private static bool DisenchantUnitCard(Profile profile, int cardID)
        {
            var success = false;
            profile.UnitsCards.ForEach(pair =>
            {
                if (!pair.Key.CardID.Equals(cardID))
                    return;
                if (pair.Value <= 0)
                    return;
                if (pair.Key.Rarity == Rarity.Basic)
                    return;

                profile.UnitsCards[pair.Key]--;
                switch (pair.Key.Rarity)
                {
                    case Rarity.Basic:
                        break;
                    case Rarity.Common:
                        success = true;
                        profile.Dust += 50;
                        break;
                    case Rarity.Rare:
                        success = true;
                        profile.Dust += 125;
                        break;
                    case Rarity.Epic:
                        success = true;
                        profile.Dust += 250;
                        break;
                    case Rarity.Legendary:
                        success = true;
                        profile.Dust += 1000;
                        break;
                    case Rarity.Mythical:
                        success = true;
                        profile.Dust += 3000;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });

            return success;
        }
        
        public static bool Craft(int cardID)
        {
            var success = CraftCommanderCard(Profile.Instance, cardID);
            if (!success)
            {
                success = CraftUnitCard(Profile.Instance, cardID);
            }
            return success;
        }
        
        private static bool CraftCommanderCard(Profile profile, int cardID)
        {
            var success = false;
            profile.CommanderCards.ForEach(pair =>
            {
                if (!pair.Key.CardID.Equals(cardID))
                    return;
                if (pair.Value <= 0)
                    return;
                int cost;
                switch (pair.Key.Rarity)
                {
                    case Rarity.Basic:
                    case Rarity.Mythical:
                        return;
                    case Rarity.Common:
                        cost = 100;
                        break;
                    case Rarity.Rare:
                        cost = 250;
                        break;
                    case Rarity.Epic:
                        cost = 500;
                        break;
                    case Rarity.Legendary:
                        cost = 2000;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                if (profile.Dust < cost) 
                    return;
                profile.Dust -= cost;
                profile.CommanderCards[pair.Key]++;
                success = true;
            });

            return success;
        }
        
        private static bool CraftUnitCard(Profile profile, int cardID)
        {
            var success = false;
            profile.UnitsCards.ForEach(pair =>
            {
                if (!pair.Key.CardID.Equals(cardID))
                    return;
                if (pair.Value <= 0)
                    return;
                int cost;
                switch (pair.Key.Rarity)
                {
                    case Rarity.Basic:
                    case Rarity.Mythical:
                        return;
                    case Rarity.Common:
                        cost = 100;
                        break;
                    case Rarity.Rare:
                        cost = 250;
                        break;
                    case Rarity.Epic:
                        cost = 500;
                        break;
                    case Rarity.Legendary:
                        cost = 2000;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                if (profile.Dust < cost) 
                    return;
                profile.Dust -= cost;
                profile.UnitsCards[pair.Key]++;
                success = true;
            });

            return success;
        }
    }
}