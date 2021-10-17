using System;
using System.Collections;
using _Project.Network.Photon.Scripts;
using TMPro;
using UnityEngine;

namespace _Project.UI.Managers.Scripts
{
    public class MasterCanvas : MonoBehaviour
    {
        #region SerialzeFields

        [SerializeField] private GameObject lostConnectionImage;
        [SerializeField] private TextMeshProUGUI errorMessageText;
        [SerializeField] private CanvasManager canvasManager;

        #endregion

        #region Private Fields

        private Coroutine _lostConnectionCo;

        #endregion

        #region Event

        public event Action OnConnectionLost;
        public event Action OnReconnected;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            PhotonConnector.OnPhotonDisconnected += LostConnection;
            PhotonConnector.OnPhotonConnected += Reconnected;
        }

        private void OnDisable()
        {
            PhotonConnector.OnPhotonDisconnected -= LostConnection;
            PhotonConnector.OnPhotonConnected -= Reconnected;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Changes interactable status of GameObjects.
        /// Displays error Message.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        private IEnumerator LostConnectionCo(string error)
        {
            lostConnectionImage.SetActive(true);
            errorMessageText.text = error;
            errorMessageText.gameObject.SetActive(true);
            canvasManager.ChangeInteractableStatus(false);

            yield return new WaitForSeconds(3f);

            lostConnectionImage.SetActive(false);
            errorMessageText.text = string.Empty;
            errorMessageText.gameObject.SetActive(false);
            canvasManager.ChangeInteractableStatus(false);
        }

        /// <summary>
        /// Start Coroutine that shows lost connection.
        /// Invokes OnConnectionLost Event
        /// </summary>
        /// <param name="errorMessage"></param>
        private void LostConnection(string errorMessage)
        {
            OnConnectionLost?.Invoke();
            if (_lostConnectionCo != null)
                StopCoroutine(_lostConnectionCo);
            
            _lostConnectionCo = StartCoroutine(LostConnectionCo(errorMessage));
        }

        /// <summary>
        /// Stops Lost connection coroutine
        /// Invokes OnReconnected Event
        /// </summary>
        private void Reconnected()
        {
            OnReconnected?.Invoke();
            if (_lostConnectionCo != null)
                StopCoroutine(_lostConnectionCo);
        }

        #endregion
    }
}
