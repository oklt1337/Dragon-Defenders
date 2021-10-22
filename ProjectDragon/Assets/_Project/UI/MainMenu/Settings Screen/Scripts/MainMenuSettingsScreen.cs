using _Project.Network.PlayFab.Scripts;
using _Project.UI.MainMenu.Manager.Scripts;
using _Project.UI.Managers.Scripts;
using _Project.Utility.SceneManager.Scripts;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.MainMenu.Settings_Screen.Scripts
{
    public class MainMenuSettingsScreen : MonoBehaviourPun, ICanvas
    {
        [SerializeField] private Button logOutButton;
        [SerializeField] private Button homeScreenButton;
        
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
        
        public void OnClickLogOut()
        {
            PlayFabAuthService.Instance.Logout();
            SceneManager.ChangeScene(Scene.Authorize);
        }

        public void OnClickHomeScreen()
        {
            MainMenuCanvasManager.Instance.HomeScreen.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void ChangeInteractableStatus(bool status)
        {
            logOutButton.interactable = status;
            homeScreenButton.interactable = status;
        }
    }
}