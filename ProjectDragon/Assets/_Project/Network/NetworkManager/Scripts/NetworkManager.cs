using System;
using _Project.Network.Photon.Scripts;
using _Project.Network.PlayFab.Scripts;
using UnityEngine;

namespace _Project.Network.NetworkManager.Scripts
{
    [DefaultExecutionOrder(-100)]
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager Instance;

        #region Serialize Field

        [SerializeField] private PhotonManager photonManager;
        
        [SerializeField] private PlayFabManager playFabManager;

        #endregion

        #region Private Fields

        private bool photonServiceConnected;

        #endregion

        #region Public Properties

        public PhotonManager PhotonManager => photonManager;
        
        public PlayFabManager PlayFabManager => playFabManager;

        #endregion

        #region Event

        public event Action OnAllServicesConnected;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            PhotonManager.OnConnectedToPhotonServices += PhotonServiceConnected;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Set photonService To connected.
        /// </summary>
        private void PhotonServiceConnected()
        {
            photonServiceConnected = true;

            if (photonServiceConnected)
            {
                OnAllServicesConnected?.Invoke();
            }
        }

        #endregion
    }
}
