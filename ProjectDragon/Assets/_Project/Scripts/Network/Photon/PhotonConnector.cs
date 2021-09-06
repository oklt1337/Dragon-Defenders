using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace _Project.Scripts.Network.Photon
{
    public class PhotonConnector : MonoBehaviourPunCallbacks
    {
        #region Private Fields

        #endregion

        #region Public Fields

        #endregion

        #region Events

        public static Action GetPhotonFriends = delegate { };

        #endregion

        #region Unity Methods

        private void Start()
        {
            var nickName = PlayerPrefs.GetString("USERNAME");

            if (!PhotonNetwork.IsConnected)
            {
                ConnectToPhoton(nickName);
            }
        }

        #endregion

        #region Private Methods

        private void ConnectToPhoton(string nickName)
        {
            Debug.Log($"Connect to Photon as {nickName}");

            PhotonNetwork.AuthValues = new AuthenticationValues(nickName);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.NickName = nickName;
            PhotonNetwork.ConnectUsingSettings();
        }

        private void CreatePhotonRoom(string roomName)
        {
            var option = new RoomOptions
            {
                MaxPlayers = 8,
                IsOpen = true,
                IsVisible = true
            };

            PhotonNetwork.JoinOrCreateRoom(roomName, option, TypedLobby.Default);
        }

        #endregion

        #region Public Methods

        #endregion

        #region Photon Callbacks

        #region IConnectionCallbacks

        public override void OnConnected()
        {
            Debug.Log("Connecting...");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogError($"Connection Lost: {cause}");
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to MasterServer.");

            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }
        }

        public override void OnRegionListReceived(RegionHandler regionHandler)
        {
            Debug.Log("RegionList Received.");
            Debug.Log($"Best Region:{regionHandler.BestRegion.ToString()}");
        }

        #endregion

        #region ILobbyCallbacks

        public override void OnJoinedLobby()
        {
            Debug.Log($"Joined Lobby: {PhotonNetwork.CurrentLobby}");
            GetPhotonFriends?.Invoke();
        }

        public override void OnLeftLobby()
        {
            Debug.Log("Left Lobby.");
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log($"RoomList Updated: RoomCount = {roomList.Count}");
        }

        public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
            Debug.Log($"LobbyStatistics Updated: LobbyCount = {lobbyStatistics.Count}");
        }

        #endregion

        #region IMatchmakingCallbacks

        public override void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            Debug.Log($"FriendList Updated: FriendCount = {friendList.Count}");
            //PhotonFriendController.OnDisplayFriends?.Invoke(friendList);
        }

        public override void OnCreatedRoom()
        {
            Debug.Log($"Created Room: {PhotonNetwork.CurrentRoom.Name}");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log($"Joined Room: {PhotonNetwork.CurrentRoom.Name}");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"ERROR {returnCode}: {message}");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.LogError($"ERROR {returnCode}: {message}");
        }

        public override void OnLeftRoom()
        {
            Debug.Log("Left Room.");
        }

        #endregion

        #region IInRoomCallbacks

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log($"{newPlayer.UserId}: Joined Room.");
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log($"{otherPlayer.UserId}: Left the Room.");
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            Debug.Log("RoomProperties Updated");
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            Debug.Log($"PlayerProperties Updated: {targetPlayer.UserId}");
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            Debug.Log($"New Master Client: {newMasterClient.UserId}");
        }

        #endregion

        #region IErrorInfoCallback

        public override void OnErrorInfo(ErrorInfo errorInfo)
        {
            Debug.LogError($"ERROR {errorInfo.Info}");
        }

        #endregion

        #endregion
    }
}
