using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Network.PlayFab.Scripts;
using _Project.Utility.SceneManager.Scripts;
using Photon.Pun;
using Photon.Realtime;
using PlayFab.ClientModels;
using Unity.VisualScripting;
using UnityEngine;
using FriendInfo = Photon.Realtime.FriendInfo;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Random = UnityEngine.Random;

namespace _Project.Network.Photon.Scripts
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
        private Coroutine pingCo;

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
            base.OnEnable();
            PlayFabAuthService.OnLoginSuccess += SetConnectionData;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            PlayFabAuthService.OnLoginSuccess -= SetConnectionData;
        }
        
        private void Awake()
        {
            ConnectToPhoton();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Connecting to Photon.
        /// </summary>
        private static void ConnectToPhoton()
        {
            var tempUserName = $"User{Random.Range(1000, 9999)}";
            
            Debug.Log($"Connect to Photon as {tempUserName}");

            // Set AppVersion.
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = AppVersion;

            // Set GameVersion.
            PhotonNetwork.GameVersion = GameVersion;

            // Creating AuthValues
            var authValues = new AuthenticationValues
            {
                UserId = tempUserName
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
            PhotonNetwork.NickName = tempUserName;
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

        /// <summary>
        /// Setting PhotonData on Login to PlayFab.
        /// </summary>
        /// <param name="loginResult">LoginResult</param>
        private void SetConnectionData(LoginResult loginResult)
        {
            Debug.Log($"Photon Data set from {loginResult.InfoResultPayload.PlayerProfile.DisplayName}");
            
            PhotonNetwork.AuthValues = new AuthenticationValues
            {
                UserId = loginResult.PlayFabId
            };
            
            // Setting nickname
            PhotonNetwork.NickName = loginResult.InfoResultPayload.PlayerProfile.DisplayName;
        }

        #endregion

        #region Public Methods

        public static void CreateRoom()
        {
            var options = new RoomOptions
            {
                IsVisible = false,
                IsOpen = false,
                MaxPlayers = 1
            };
            PhotonNetwork.CreateRoom(string.Empty, options, TypedLobby.Default);
        }

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
            if (pingCo != null)
                StopCoroutine(pingCo);
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

            CreateRoom();
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

            if (SceneManager.CurrentScene != Scene.Lobby)
                return;
            SceneManager.ChangeScene(Scene.MainMenu);

            // Make sure coroutine doesnt run twice.
            if (pingCo != null)
                StopCoroutine(pingCo);

            pingCo = StartCoroutine(SetPingCo());
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
