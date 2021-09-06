using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

namespace _Project.Scripts.Network.Photon
{
    public class PhotonFriendConnector : MonoBehaviourPunCallbacks
    {
        #region Private Fields

        #endregion

        #region Public Fields

        #endregion

        #region Events

        public static Action<List<global::Photon.Realtime.FriendInfo>> OnDisplayFriends = delegate { };

        #endregion

        #region Unity Methods

        private void Awake()
        {
            //PlayFabFriendController.OnFriendListUpdated += HandleFriendsUpdated;
        }

        private void OnDestroy()
        {
            //PlayFabFriendController.OnFriendListUpdated -= HandleFriendsUpdated;
        }

        #endregion

        #region Private Methods

        private void HandleFriendsUpdated(List<FriendInfo> friends)
        {
            if (friends.Count != 0)
            {
                //var friendDisplayNames = friends.Select(f => f.TitleDisplayName).ToArray();
                //PhotonNetwork.FindFriends(friendDisplayNames);
            }
            else
            {
                var photonFriends = new List<global::Photon.Realtime.FriendInfo>();
                OnDisplayFriends?.Invoke(photonFriends);
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Photon Callbacks

        #endregion
    }
}
