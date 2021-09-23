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

        #region Protected Fields

        

        #endregion

        #region Public Fields

        

        #endregion

        #region Public Properties

        

        #endregion

        #region Events

        

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

        #region Public Methods

        

        #endregion
    }
}
