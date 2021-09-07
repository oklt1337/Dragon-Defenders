using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Network.Photon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class MasterCanvas : MonoBehaviour
    {
        public static MasterCanvas Instance;

        [SerializeField] private GameObject lostConnectionImage;
        [SerializeField] private TextMeshProUGUI errorMessageText;
        
        private ICanvas _canvasManager;

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

        private void Start()
        {
            PhotonConnector.OnDisconnectedFromPhoton += LostConnection;
        }

        private void OnDestroy()
        {
            PhotonConnector.OnDisconnectedFromPhoton -= LostConnection;
        }

        #endregion

        #region private Methods

        private IEnumerator LostConnectionCo(string error)
        {
            lostConnectionImage.SetActive(true);
            errorMessageText.text = error;
            errorMessageText.gameObject.SetActive(true);
            _canvasManager?.ChangeInteractableStatus(false);

            yield return new WaitForSeconds(3f);
            
            lostConnectionImage.SetActive(false);
            errorMessageText.text = string.Empty;
            errorMessageText.gameObject.SetActive(false);
            _canvasManager?.ChangeInteractableStatus(true);
        }
        
        /// <summary>
        /// Start Coroutine that shows lost connection.
        /// </summary>
        /// <param name="errorMessage"></param>
        private void LostConnection(string errorMessage)
        {
            StartCoroutine(LostConnectionCo(errorMessage));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set Current Canvas Manager
        /// </summary>
        /// <param name="canvasManager"></param>
        public void SignAsCurrentCanvasManager(ICanvas canvasManager)
        {
            _canvasManager = canvasManager;
        }

        /// <summary>
        /// Remove Current CanvasManager
        /// </summary>
        public void RemoveCurrentCanvasManager()
        {
            _canvasManager = null;
        }

        #endregion
    }
}
