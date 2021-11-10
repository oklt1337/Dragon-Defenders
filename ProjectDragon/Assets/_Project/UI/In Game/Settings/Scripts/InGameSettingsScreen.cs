using GamePlay.GameManager.Scripts;
using GamePlay.Player.PlayerModel.Scripts;
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
            GameManager.Instance.PlayerModel.ChangeState(State.Blocked);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
            GameManager.Instance.PlayerModel.ChangeState(State.Idle);
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