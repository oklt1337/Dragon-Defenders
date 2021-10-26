using _Project.Deck_Cards.DeckManager.Scripts;
using _Project.UI.Lobby.Manager.Scripts;
using _Project.UI.MainMenu.Manager.Scripts;
using _Project.UI.Managers.Scripts;
using _Project.Utility.SceneManager.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.New_Deck_Screen.Scripts
{
    public class NewDeckScreen : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button createButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private TextMeshProUGUI nameField;

        private void OnEnable()
        {
            CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
        }
        
        /// <summary>
        /// Uses the input field to create a deck.
        /// </summary>
        public void OnCreateClick()
        {
            if (!DeckManager.Instance.CreateDeck(nameField.text))
                return;
            
            gameObject.SetActive(false);
            switch (SceneManager.CurrentScene)
            {
                case Scene.Intro:
                    break;
                case Scene.Authorize:
                    break;
                case Scene.MainMenu:
                    MainMenuCanvasManager.Instance.DeckManagerScreen.ChangeInteractableStatus(true);
                    break;
                case Scene.Lobby:
                    LobbyCanvasManager.Instance.DeckManagerScreen.ChangeInteractableStatus(true);
                    break;
                case Scene.GameScene:
                    break;
            }
        }

        /// <summary>
        /// Closes the Screen and makes the Deck Manager interactable.
        /// </summary>
        public void OnCancelClick()
        {
            gameObject.SetActive(false);
            switch (SceneManager.CurrentScene)
            {
                case Scene.Intro:
                    break;
                case Scene.Authorize:
                    break;
                case Scene.MainMenu:
                    MainMenuCanvasManager.Instance.DeckManagerScreen.ChangeInteractableStatus(true);
                    break;
                case Scene.Lobby:
                    LobbyCanvasManager.Instance.DeckManagerScreen.ChangeInteractableStatus(true);
                    break;
                case Scene.GameScene:
                    break;
            }
        }

        public void ChangeInteractableStatus(bool status)
        {
            createButton.interactable = status;
            cancelButton.interactable = status;
        }
    }
}