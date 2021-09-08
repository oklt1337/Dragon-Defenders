using System;
using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using UnityEngine;

namespace _Project.Scripts.Network.Photon
{
    public class PhotonChatConnector : MonoBehaviour, IChatClientListener
    {
        #region Private Serializable Fields

        #endregion

        #region Private Fields

        private ChatClient _chatClient;

        #endregion

        #region Public Fields

        #endregion

        #region Events

        public static event Action OnChatConnected;
        public static event Action OnChatDisconnected;
        public static event Action<DebugLevel, string> OnChatDebugReturn;
        public static event Action<ChatState> OnChatStateChanged;
        public static event Action<string, string[], object[]> OnChatMessageReceived;
        public static event Action<string, object, string> OnChatPrivateMessage;
        public static event Action<string[], bool[]> OnChatSubscribed;
        public static event Action<string[]> OnChatUnsubscribed;
        public static event Action<string, int, bool, object> OnChatStatusUpdated;
        public static event Action<string, string> OnChatUserSubscribed;
        public static event Action<string, string> OnChatUserUnsubscribed;
        
        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            PhotonConnector.OnPhotonConnected += ConnectToPhotonChat;
        }

        private void Start()
        {
            _chatClient = new ChatClient(this);
        }

        private void Update()
        {
            // make sure we receive massages and being connected.
            _chatClient.Service();
        }

        private void OnDisable()
        {
            PhotonConnector.OnPhotonConnected -= ConnectToPhotonChat;
        }

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Connecting Client to Photon Chat.
        /// </summary>
        private void ConnectToPhotonChat()
        {
            Debug.Log("Connecting to Photon Chat.");
            
            AuthenticationValues authValues = new AuthenticationValues
            {
                UserId = PhotonNetwork.LocalPlayer.UserId
            };
            _chatClient.AuthValues = authValues;

            _chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion, _chatClient.AuthValues);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sending Private Message.
        /// </summary>
        /// <param name="recipient">string</param>
        /// <param name="message">string</param>
        public void SendDirectMessage(string recipient, string message)
        {
            _chatClient.SendPrivateMessage(recipient, message);
        }
        
        #endregion

        #region Photon Chat Callbacks

        public void DebugReturn(DebugLevel level, string message)
        {
            switch (level)
            {
                case DebugLevel.OFF:
                    Debug.Log($"New OFF Debug: {message}");
                    break;
                case DebugLevel.ERROR:
                    Debug.LogError($"ERROR: {message}");
                    break;
                case DebugLevel.WARNING:
                    Debug.LogWarning($"Warning: {message}");
                    break;
                case DebugLevel.INFO:
                    Debug.Log($"New INFO Debug: {message}");
                    break;
                case DebugLevel.ALL:
                    Debug.Log($"New ALL Debug: {message}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
            
            OnChatDebugReturn?.Invoke(level, message);
        }

        public void OnDisconnected()
        {
            Debug.Log("Disconnected from Chat.");
            OnChatDisconnected?.Invoke();
        }

        public void OnConnected()
        {
            Debug.Log("Connected to Chat.");
            OnChatConnected?.Invoke();
        }

        public void OnChatStateChange(ChatState state)
        {
            Debug.Log($"ChatState changed to {state}.");
            OnChatStateChanged?.Invoke(state);
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            Debug.Log($"Received new Message in Channel {channelName}");
            OnChatMessageReceived?.Invoke(channelName, senders, messages);
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            Debug.Log($"Received new Private Message in Channel {channelName}");
            OnChatPrivateMessage?.Invoke(sender, message, channelName);
            
            if (!string.IsNullOrEmpty(message.ToString()))
            {
                // Channel Name format [Sender : Recipient]
                var splitNames = channelName.Split(':');
                var senderName = splitNames[0];

                if (!sender.Equals(senderName, StringComparison.OrdinalIgnoreCase))
                {
                    Debug.Log($"{sender}: {message}");
                }
            }
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            Debug.Log("Subscribed to new channel.");
            OnChatSubscribed?.Invoke(channels, results);
        }

        public void OnUnsubscribed(string[] channels)
        {
            Debug.Log("Unsubscribed from a channel.");
            OnChatUnsubscribed?.Invoke(channels);
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            Debug.Log($"Updated Status of {user}: {status}");
            OnChatStatusUpdated?.Invoke(user, status, gotMessage, message);
        }

        public void OnUserSubscribed(string channel, string user)
        {
            Debug.Log($"{user} subscribed to new channel: {channel}.");
            OnChatUserSubscribed?.Invoke(channel, user);
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
            Debug.Log($"{user} unsubscribed from channel: {channel}.");
            OnChatUserUnsubscribed?.Invoke(channel, user);
        }

        #endregion
    }
}
