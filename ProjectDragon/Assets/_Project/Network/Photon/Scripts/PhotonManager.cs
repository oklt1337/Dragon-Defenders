using UnityEngine;

namespace _Project.Network.Photon.Scripts
{
    public class PhotonManager : MonoBehaviour
    {
        #region Private Fields

        [SerializeField] 
        private PhotonConnector photonConnector;
        
        [SerializeField] 
        private PhotonChatConnector photonChatConnector;
        
        [SerializeField] 
        private PhotonFriendHandler photonFriendHandler;

        #endregion

        #region public Properies

        public PhotonConnector PhotonConnector => photonConnector;
        public PhotonChatConnector PhotonChatConnector => photonChatConnector;
        public PhotonFriendHandler PhotonFriendHandler => photonFriendHandler;

        #endregion
    }
}
