using System;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ProfilesModels;
using UnityEngine;

namespace _Project.Network.PlayFab.Scripts
{
    public class PlayFabManager : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private PlayFabFriendsHandler playFabFriendsHandler;
        [SerializeField] private PlayFabLootboxHandler playFabLootBoxHandler;
        [SerializeField] private PlayFabProfileHandler playFabProfileHandler;
        [SerializeField] private PlayFabSpecialEventsHandler playFabSpecialEventsHandler;
        [SerializeField] private PlayFabMonetizationHandler playFabMonetizationHandler;
        [SerializeField] private PlayFabBadWordsFilter playFabBadWordsFilter;
        [SerializeField] private PlayFabAuthManager playFabAuthManager;

        #endregion

        #region Public Properies
        
        public PlayFabFriendsHandler PlayFabFriendsHandler => playFabFriendsHandler;
        public PlayFabLootboxHandler PlayFabLootBoxHandler => playFabLootBoxHandler;
        public PlayFabProfileHandler PlayFabProfileHandler => playFabProfileHandler;
        public PlayFabSpecialEventsHandler PlayFabSpecialEventsHandler => playFabSpecialEventsHandler;
        public PlayFabMonetizationHandler PlayFabMonetizationHandler => playFabMonetizationHandler;
        public PlayFabBadWordsFilter PlayFabBadWordsFilter => playFabBadWordsFilter;
        public PlayFabAuthManager PlayFabAuthManager => playFabAuthManager;

        #endregion
    }
}
