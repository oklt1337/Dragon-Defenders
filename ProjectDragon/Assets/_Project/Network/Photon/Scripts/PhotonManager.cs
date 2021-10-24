using System;
using UnityEngine;

namespace _Project.Network.Photon.Scripts
{
    public class PhotonManager : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private PhotonConnector photonConnector;
        
        [SerializeField] private PhotonChatConnector photonChatConnector;
        
        [SerializeField] private PhotonFriendHandler photonFriendHandler;

        #endregion

        #region Private Fields

        private bool photonConnectorServiceConnected;
        private bool photonChatConnectorServiceConnected;
        private bool photonFriendHandlerServiceConnected;

        #endregion

        #region Public Properies

        public PhotonConnector PhotonConnector => photonConnector;
        public PhotonChatConnector PhotonChatConnector => photonChatConnector;
        public PhotonFriendHandler PhotonFriendHandler => photonFriendHandler;

        #endregion

        #region Events

        public event Action OnConnectedToPhotonServices;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            PhotonConnector.OnPhotonConnected += ConnectedToPhotonConnectorService;
            PhotonChatConnector.OnChatConnected += ConnectedToPhotonChatConnectorService;
            PhotonFriendHandler.OnConnectedToPhotonFriends += ConnectedToPhotonFriendHandlerService;
        }

        #endregion

        #region Private Methods
        
        private void ConnectedToPhotonConnectorService()
        {
            Debug.Log("PhotonConnector connected");
            photonConnectorServiceConnected = true;

            if (photonChatConnectorServiceConnected && photonFriendHandlerServiceConnected)
            {
                OnConnectedToPhotonServices?.Invoke();
            }
        }
        
        private void ConnectedToPhotonChatConnectorService()
        {
            Debug.Log("PhotonChatConnector connected");
            photonChatConnectorServiceConnected = true;

            if (photonConnectorServiceConnected && photonFriendHandlerServiceConnected)
            {
                OnConnectedToPhotonServices?.Invoke();
            }
        }
        
        private void ConnectedToPhotonFriendHandlerService()
        {
            Debug.Log("PhotonFriendsHandler connected");
            photonFriendHandlerServiceConnected = true;

            if (photonChatConnectorServiceConnected && photonConnectorServiceConnected)
            {
                OnConnectedToPhotonServices?.Invoke();
            }
        }

        #endregion
    }
}
