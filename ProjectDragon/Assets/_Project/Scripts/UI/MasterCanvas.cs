using System.Collections;
using _Project.Scripts.Network.Photon;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class MasterCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject lostConnectionImage;
        [SerializeField] private TextMeshProUGUI errorMessageText;

        [SerializeField] private CanvasManager canvasManager;

        private Coroutine _lostConnectionCo;

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

        #region private Methods

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
        /// </summary>
        /// <param name="errorMessage"></param>
        private void LostConnection(string errorMessage)
        {
            if (_lostConnectionCo != null)
                StopCoroutine(_lostConnectionCo);
            
            _lostConnectionCo = StartCoroutine(LostConnectionCo(errorMessage));
        }

        private void Reconnected()
        {
            if (_lostConnectionCo != null)
                StopCoroutine(_lostConnectionCo);
        }

        #endregion
    }
}
