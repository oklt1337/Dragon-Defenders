using _Project.UI.Lobby.Manager.Scripts;
using UI.MainMenu.Manager.Scripts;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Utility.SceneManager.Scripts;

namespace UI.Deck_Manager_Screen.Scripts
{
    public class DeckManagerScreen : MonoBehaviour, ICanvas
    {
        public Button closeButton;
        public Button addDeckButton;
        
        private void OnEnable()
        {
            CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
        }
        
        /// <summary>
        /// Closes the Deck Manager Screen and opens the correct screen.
        /// </summary>
        public void OnCloseClick()
        {
            if (SceneManager.CurrentScene == Scene.MainMenu)
            {
                MainMenuCanvasManager.Instance.HomeScreen.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
            else if (SceneManager.CurrentScene == Scene.Lobby)
            {
                LobbyCanvasManager.Instance.LobbyScreen.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Opens the new deck screen and turns itself off.
        /// </summary>
        public void OnAddClick()
        {
            if (SceneManager.CurrentScene == Scene.MainMenu)
            {
                MainMenuCanvasManager.Instance.NewDeckScreen.gameObject.SetActive(true);
                ChangeInteractableStatus(false);
            }
            else if (SceneManager.CurrentScene == Scene.Lobby)
            {
                LobbyCanvasManager.Instance.NewDeckScreen.gameObject.SetActive(true);
                ChangeInteractableStatus(false);
            }
        }

        public void ChangeInteractableStatus(bool status)
        {
            closeButton.interactable = status;
            addDeckButton.interactable = status;
        }
    }
}