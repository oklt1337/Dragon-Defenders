using _Project.Network.Photon.Scripts;
using _Project.Network.PlayFab.Scripts;
using UnityEngine;

namespace _Project.Network.NetworkManager.Scripts
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager Instance;

        #region Private Fields

        [SerializeField] 
        private PhotonManager photonManager;
        
        [SerializeField] 
        private PlayFabManager playFabManager;

        #endregion

        #region Public Properties

        public PhotonManager PhotonManager => photonManager;
        
        public PlayFabManager PlayFabManager => playFabManager;

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
        }

        #endregion
    }
}
