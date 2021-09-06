using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace _Project.Scripts.Network.Photon
{
    public class PhotonConnector : MonoBehaviourPunCallbacks
    {
        #region Private Fields

        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion.
        /// </summary>
        private const string GameVersion = "1.0.0";

        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion.
        /// </summary>
        private const string AppVersion = "1.0.0";

        /// <summary>
        /// Coroutine for setting the ping.
        /// </summary>
        private Coroutine _pingCo;

        #endregion

        #region Public Fields

        #endregion

        #region Events

        public event Action OnConnectedToPhoton;

        #endregion

        #region Unity Methods

        private void Start()
        {
            NetworkManager.Instance.PlayFabManager.PlayFabLogin.OnLoginSuccess += ConnectToPhoton;
        }

        #endregion

        #region Private Methods

        private void ConnectToPhoton(string displayName, string id)
        {
            Debug.Log($"Connect to Photon as {displayName}");

            // Set AppVersion.
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = AppVersion;

            // Set GameVersion.
            PhotonNetwork.GameVersion = GameVersion;

            // Creating AuthValues
            var authValues = new AuthenticationValues
            {
                UserId = id
            };
            
            // Setting AuthValues
            PhotonNetwork.AuthValues = authValues;

            // Connecting to Photon
            PhotonNetwork.ConnectUsingSettings();

            // Make sure if LoadLevel is called all clients sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;

            // Make sure on new connection hashtable is empty
            PhotonNetwork.LocalPlayer.CustomProperties = new Hashtable();

            // Setting nickname
            PhotonNetwork.NickName = displayName;
        }

        private void CreatePhotonRoom()
        {
            var option = new RoomOptions
            {
                MaxPlayers = 1,
                IsOpen = false,
                IsVisible = false
            };

            PhotonNetwork.CreateRoom("", option, TypedLobby.Default);
        }

        /// <summary>
        /// Coroutine to set ping in LocalPlayer.CustomProperties
        /// </summary>
        /// <returns></returns>
        private static IEnumerator SetPingCo()
        {
            while (PhotonNetwork.IsConnected)
            {
                if (!PhotonNetwork.IsConnectedAndReady || !PhotonNetwork.InRoom) continue;

                var hashtable = PhotonNetwork.LocalPlayer.CustomProperties;
                if (!hashtable.ContainsKey("Ping"))
                {
                    hashtable.Add("Ping", PhotonNetwork.GetPing());
                }
                else
                {
                    hashtable["Ping"] = PhotonNetwork.GetPing();
                }

                PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);

                yield return new WaitForSeconds(1f);
            }
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
            // Make sure coroutine doesnt run twice.
            if (_pingCo != null)
                StopCoroutine(_pingCo);
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
        }

        public override void OnCreatedRoom()
        {
            Debug.Log($"Created Room: {PhotonNetwork.CurrentRoom.Name}");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log($"Joined Room: {PhotonNetwork.CurrentRoom.Name}");

            // Make sure coroutine doesnt run twice.
            if (_pingCo != null)
                StopCoroutine(_pingCo);

            _pingCo = StartCoroutine(SetPingCo());
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
