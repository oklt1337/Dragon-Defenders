using System.Collections.Generic;
using PlayFab.ClientModels;
using Sirenix.Serialization;
using UnityEngine;
using Profile = PlayerProfile.Profile.Scripts.Profile;

namespace _Project.Network.PlayFab.Scripts
{
    public class PlayFabProfileHandler : MonoBehaviour
    {
        public Profile PlayerProfile { get; } = Profile.Instance;

        #region Unity Methods

        private void Awake()
        {
            PlayFabAuthService.OnLoginSuccess += InitializeProfile;
        }

        private void OnDestroy()
        {
            PlayFabAuthService.OnLoginSuccess -= InitializeProfile;
        }

        #endregion

        #region Public Metods

        public void InitializeProfile(LoginResult result)
        {
            if (result.InfoResultPayload.CharacterInventories != null)
            {
                SetCharacterInventoryList(result.InfoResultPayload.CharacterInventories);
            }

            if (result.InfoResultPayload.CharacterList != null)
            {
                SetCharacterList(result.InfoResultPayload.CharacterList);
            }
            
            if (result.InfoResultPayload.PlayerProfile != null)
            {
                SetPlayerProfile(result.InfoResultPayload.PlayerProfile);
            }
            
            if (result.InfoResultPayload.PlayerStatistics != null)
            {
                SetPlayerStatistics(result.InfoResultPayload.PlayerStatistics);
            }
            
            if (result.InfoResultPayload.TitleData != null)
            {
                SetTileData(result.InfoResultPayload.TitleData);
            }
        }

        #endregion

        #region Private Methods

        private void SetCharacterInventoryList(List<CharacterInventory> characterInventories)
        {
            PlayerProfile.CharacterInventories = characterInventories;
        }

        private void SetCharacterList(List<CharacterResult> characterResults)
        {
            PlayerProfile.CharacterResults = characterResults;
        }

        private void SetPlayerProfile(PlayerProfileModel playerProfileModel)
        {
            PlayerProfile.ProfileModel = playerProfileModel;
        }

        private void SetPlayerStatistics(List<StatisticValue> playerStatistics)
        {
            PlayerProfile.PlayerStatistics = playerStatistics;
        }

        private void SetTileData(Dictionary<string, string> titleData)
        {
            PlayerProfile.TitleData = titleData;
        }

        #endregion
    }
}