using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Network.PlayFab;
using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Random = UnityEngine.Random;

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

        public static event Action OnPhotonConnected;
        public static event Action<string> OnPhotonDisconnected;
        public static event Action OnConnectedToPhotonMaster;
        public static event Action<RegionHandler> OnPhotonRegionListReceived;
        public static event Action OnPhotonJoinedLobby;
        public static event Action OnPhotonLeftLobby;
        public static event Action<List<RoomInfo>> OnPhotonRoomListUpdated;
        public static event Action<List<TypedLobbyInfo>> OnPhotonLobbyStatisticsUpdated;
        public static event Action<List<FriendInfo>> OnPhotonFriendListUpdated;
        public static event Action OnPhotonRoomCreated;
        public static event Action OnPhotonJoinedRoom;
        public static event Action<short, string> OnPhotonJoinedRoomFailed;
        public static event Action<short, string> OnPhotonJoinedRandomFailed;
        public static event Action OnPhotonLeftRoom;
        public static event Action<Player> OnPhotonPlayerEnterRoom;
        public static event Action<Player> OnPhotonPlayerLeftRoom;
        public static event Action<Hashtable> OnPhotonRoomPropertiesUpdated;
        public static event Action<Player, Hashtable> OnPhotonPlayerPropertiesUpdated;
        public static event Action<Player> OnPhotonMasterClientSwitched;
        public static event Action<ErrorInfo> OnPhotonErrorInfo;

        #endregion

        #region Unity Methods

        public override void OnEnable()
        {
            PlayFabLogin.OnLoginSuccess += ConnectWithPlayFabData;
        }
        
        private void Start()
        {
            if (PhotonNetwork.IsConnected)
                return;
            ConnectToPhoton(null, null);
        }

        public override void OnDisable()
        {
            PlayFabLogin.OnLoginSuccess -= ConnectWithPlayFabData;
        }

        #endregion

        #region Private Methods

        private void ConnectWithPlayFabData(string displayName, string id)
        {
            Debug.Log("Connecting with PlayFabData.");
            
            if (!PhotonNetwork.IsConnected)
                return;

            PhotonNetwork.Disconnect();
            ConnectToPhoton(displayName, id);
        }

        private void ConnectToPhoton([CanBeNull] string displayName, [CanBeNull] string id)
        {
            if (string.IsNullOrEmpty(displayName))
            {
                displayName = "Guest#" + Random.Range(0, 9999);
                id = displayName;
            }

            Debug.Log($"Connect to Photon as {displayName}");

            // Set AppVersion.
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = AppVersion;

            // Set GameVersion.
            PhotonNetwork.GameVersion = GameVersion;
            
            // Creating AuthValues
            AuthenticationValues authValues = new AuthenticationValues
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


            // Setting AuthValues
            PhotonNetwork.AuthValues = authValues;
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
            OnPhotonConnected?.Invoke();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogError($"Connection Lost: {cause}");
            OnPhotonDisconnected?.Invoke(cause.ToString());

            // Make sure coroutine doesnt run twice.
            if (_pingCo != null)
                StopCoroutine(_pingCo);
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to MasterServer.");
            OnConnectedToPhotonMaster?.Invoke();

            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }
        }

        public override void OnRegionListReceived(RegionHandler regionHandler)
        {
            Debug.Log("RegionList Received.");
            Debug.Log($"Best Region:{regionHandler.BestRegion.ToString()}");
            OnPhotonRegionListReceived?.Invoke(regionHandler);
        }

        #endregion

        #region ILobbyCallbacks

        public override void OnJoinedLobby()
        {
            Debug.Log($"Joined Lobby: {PhotonNetwork.CurrentLobby}");
            OnPhotonJoinedLobby?.Invoke();
        }

        public override void OnLeftLobby()
        {
            Debug.Log("Left Lobby.");
            OnPhotonLeftLobby?.Invoke();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log($"RoomList Updated: RoomCount = {roomList.Count}");
            OnPhotonRoomListUpdated?.Invoke(roomList);
        }

        public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
            Debug.Log($"LobbyStatistics Updated: LobbyCount = {lobbyStatistics.Count}");
            OnPhotonLobbyStatisticsUpdated?.Invoke(lobbyStatistics);
        }

        #endregion

        #region IMatchmakingCallbacks

        public override void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            Debug.Log($"FriendList Updated: FriendCount = {friendList.Count}");
            OnPhotonFriendListUpdated?.Invoke(friendList);
        }

        public override void OnCreatedRoom()
        {
            Debug.Log($"Created Room: {PhotonNetwork.CurrentRoom.Name}");
            OnPhotonRoomCreated?.Invoke();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log($"Joined Room: {PhotonNetwork.CurrentRoom.Name}");
            OnPhotonJoinedRoom?.Invoke();

            // Make sure coroutine doesnt run twice.
            if (_pingCo != null)
                StopCoroutine(_pingCo);

            _pingCo = StartCoroutine(SetPingCo());
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"ERROR {returnCode}: {message}");
            OnPhotonJoinedRoomFailed?.Invoke(returnCode, message);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.LogError($"ERROR {returnCode}: {message}");
            OnPhotonJoinedRandomFailed?.Invoke(returnCode, message);
        }

        public override void OnLeftRoom()
        {
            Debug.Log("Left Room.");
            OnPhotonLeftRoom?.Invoke();
        }

        #endregion

        #region IInRoomCallbacks

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log($"{newPlayer.UserId}: Joined Room.");
            OnPhotonPlayerEnterRoom?.Invoke(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log($"{otherPlayer.UserId}: Left the Room.");
            OnPhotonPlayerLeftRoom?.Invoke(otherPlayer);
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            Debug.Log("RoomProperties Updated");
            OnPhotonRoomPropertiesUpdated?.Invoke(propertiesThatChanged);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            Debug.Log($"PlayerProperties Updated: {targetPlayer.UserId}");
            OnPhotonPlayerPropertiesUpdated?.Invoke(targetPlayer, changedProps);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            Debug.Log($"New Master Client: {newMasterClient.UserId}");
            OnPhotonMasterClientSwitched?.Invoke(newMasterClient);
        }

        #endregion

        #region IErrorInfoCallback

        public override void OnErrorInfo(ErrorInfo errorInfo)
        {
            Debug.LogError($"ERROR {errorInfo.Info}");
            OnPhotonErrorInfo?.Invoke(errorInfo);
        }

        #endregion

        #endregion
    }
}
