using System;
using UnityEngine;

namespace Network.Photon.Scripts
{
    public class PhotonFriendHandler : MonoBehaviour
    {
        public static event Action OnConnectedToPhotonFriends;
        
        private void Awake()
        {
            PhotonConnector.OnPhotonConnected += ConnectToPhotonFriends;
        }

        private void OnDestroy()
        {
            PhotonConnector.OnPhotonConnected -= ConnectToPhotonFriends;
        }

        private void ConnectToPhotonFriends()
        {
            OnConnectedToPhotonFriends?.Invoke();
        }
    }
}
