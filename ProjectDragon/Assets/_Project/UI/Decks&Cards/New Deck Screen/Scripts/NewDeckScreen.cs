using _Project.UI.Lobby.Manager.Scripts;
using Deck_Cards.Cards.CommanderCard.Scripts;
using Deck_Cards.DeckManager.Scripts;
using TMPro;
using UI.MainMenu.Manager.Scripts;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Utility.SceneManager.Scripts;

namespace UI.New_Deck_Screen.Scripts
{
    public class NewDeckScreen : MonoBehaviour, ICanvas
    {
        [SerializeField] private CommanderCard commanderCard;
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
                    MainMenuCanvasManager.Instance.DeckManagerScreen.UpdateDecks();
                    break;
                case Scene.Lobby:
                    LobbyCanvasManager.Instance.DeckManagerScreen.ChangeInteractableStatus(true);
                    LobbyCanvasManager.Instance.DeckManagerScreen.UpdateDecks();
                    break;
                case Scene.GameScene:
                    break;
            }
            
            // Only for Prototype, pls don't ever look at this.
            DeckManager.Instance.Decks[DeckManager.Instance.Decks.Count - 1].AddCard(commanderCard);
            DeckManager.Instance.SaveDeck(DeckManager.Instance.Decks[DeckManager.Instance.Decks.Count - 1]);
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