using _Project.Scripts.Network.Photon;
using _Project.Scripts.Network.PlayFab;
using UnityEngine;

namespace _Project.Scripts.Network
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
        

        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
    }
}
