using System;
using System.Collections.Generic;
using PlayFab.ClientModels;

namespace _Project.PlayerProfile.Profile.Scripts
{
    [Serializable]
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
    }
}
