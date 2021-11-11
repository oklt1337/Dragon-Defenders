using System.Collections.Generic;
using Deck_Cards.Cards.CommanderCard.Scripts;
using Deck_Cards.Cards.UnitCard.Scripts;
using PlayFab.ClientModels;

namespace PlayerProfile.Profile.Scripts
{
    public class Profile
    {
        public static Profile Instance => instance ??= new Profile();
        private static Profile instance;

        #region Constructor

        private Profile()
        {
            
        }
        
        #endregion

        public List<CharacterInventory> CharacterInventories { get; set; }
        public List<CharacterResult> CharacterResults { get; set; }
        public PlayerProfileModel ProfileModel { get; set; }
        public List<StatisticValue> PlayerStatistics { get; set; }
        public Dictionary<string,string> TitleData { get; set; }
        
        public int Dust { get; set; }
        public Dictionary<CommanderCard,int> CommanderCards { get; set; }
        public Dictionary<UnitCard,int> UnitsCards { get; set; }
    }
}
