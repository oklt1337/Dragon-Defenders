using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Utility.SceneManager.Scripts;

namespace UI.In_Game.Settings.Scripts
{
    public class InGameSettingsScreen : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button closeSettingsButton;
        [SerializeField] private Button toLobbyButton;

        #region Unity Methods

        private void OnEnable()
        {
            CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
        }

        #endregion

        #region Public Methods

        public void ChangeInteractableStatus(bool status)
        {
            closeSettingsButton.interactable = status;
            toLobbyButton.interactable = status;
        }

        public void OnCloseSettingsClick()
        {
            gameObject.SetActive(false);
        }

        public void OnLobbyClick()
        {
            SceneManager.ChangeScene(Scene.Lobby);
        }

        #endregion
    }
}