using TMPro;
using UI.MainMenu.Manager.Scripts;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Utility.SceneManager.Scripts;

namespace UI.MainMenu.Home_Screen.Scripts
{
    public class HomeScreen : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button lobbyButton;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button deckManagerButton;
        [SerializeField] private Button profileButton;
        [SerializeField] private Button friendListButton;
        [SerializeField] private Button eggHatchingButton;
        [SerializeField] private Button notificationButton;

        [SerializeField] private TextMeshProUGUI currency;
        
        private void OnEnable()
        {
            CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
        }
        
        public void ChangeInteractableStatus(bool status)
        {
            lobbyButton.interactable = status;
            shopButton.interactable = status;
            settingsButton.interactable = status;
            deckManagerButton.interactable = status;
            profileButton.interactable = status;
            friendListButton.interactable = status;
            eggHatchingButton.interactable = status;
            notificationButton.interactable = status;
        }

        public void OnClickLobby()
        {
            SceneManager.ChangeScene(Scene.Lobby);
        }

        public void OnClickSettings()
        {
            MainMenuCanvasManager.Instance.MainMenuSettingsScreen.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnClickCards()
        {
            MainMenuCanvasManager.Instance.DeckManagerScreen.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
