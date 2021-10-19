using UnityEngine;

namespace _Project.Network.PlayFab.Scripts
{
    public class PlayFabManager : MonoBehaviour
    {
        #region Private Fields

        [SerializeField] 
        private PlayFabFriendsHandler playFabFriendsHandler;
        
        [SerializeField] 
        private PlayFabLootboxHandler playFabLootBoxHandler;
        
        [SerializeField] 
        private PlayFabProfileHandler playFabProfileHandler;
        
        [SerializeField] 
        private PlayFabSpecialEventsHandler playFabSpecialEventsHandler;
        
        [SerializeField] 
        private PlayFabMonetizationHandler playFabMonetizationHandler;
        
        [SerializeField] 
        private PlayFabBadWordsFilter playFabBadWordsFilter;

        #endregion

        #region public Properies
        
        public PlayFabFriendsHandler PlayFabFriendsHandler => playFabFriendsHandler;
        public PlayFabLootboxHandler PlayFabLootBoxHandler => playFabLootBoxHandler;
        public PlayFabProfileHandler PlayFabProfileHandler => playFabProfileHandler;
        public PlayFabSpecialEventsHandler PlayFabSpecialEventsHandler => playFabSpecialEventsHandler;
        public PlayFabMonetizationHandler PlayFabMonetizationHandler => playFabMonetizationHandler;
        public PlayFabBadWordsFilter PlayFabBadWordsFilter => playFabBadWordsFilter;

        #endregion
    }
}
