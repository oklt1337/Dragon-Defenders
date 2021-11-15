using System.Collections.Generic;
using _Project.UI.Lobby.Manager.Scripts;
using Deck_Cards.DeckManager.Scripts;
using TMPro;
using UI.Deck_Preview.Scripts;
using UI.MainMenu.Manager.Scripts;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Utility.SceneManager.Scripts;

namespace UI.Deck_Manager_Screen.Scripts
{
    public class DeckManagerScreen : MonoBehaviour, ICanvas
    {
        [Header("Deck View")] 
        [SerializeField] private GameObject deckView;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button addDeckButton;
        [SerializeField] private List<Button> deckButtons;

        [SerializeField] private PreviewDeckPanel previewDeckPanel;

        [Header("Card View")] 
        [SerializeField] private GameObject cardView;
        [SerializeField] private Button deckViewButton;
        [SerializeField] private List<AddCardButton> addCardButtons;

        #region Unity Methods

        public void Start()
        {
            for (int i = 0; i < DeckManager.Instance.Decks.Count; i++)
            {
                var text = deckButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                text.text = DeckManager.Instance.Decks[i].DeckName;

                if(DeckManager.Instance.Decks[i].CommanderCard != null)
                    deckButtons[i].image.sprite = DeckManager.Instance.Decks[i].CommanderCard.icon;
            }
        }

        private void OnEnable()
        {
            CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
        }

        #endregion

        #region Button Methods

        /// <summary>
        /// Closes the Deck Manager Screen and opens the correct screen.
        /// </summary>
        public void OnCloseClick()
        {
            previewDeckPanel.SetPreviewDeck(null);
            previewDeckPanel.gameObject.SetActive(false);

            if (SceneManager.CurrentScene == Scene.MainMenu)
            {
                MainMenuCanvasManager.Instance.HomeScreen.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
            else
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
            else
            {
                LobbyCanvasManager.Instance.NewDeckScreen.gameObject.SetActive(true);
                ChangeInteractableStatus(false);
            }
        }

        /// <summary>
        /// Changes the active Deck.
        /// </summary>
        public void OnClickDeck(Button btn)
        {
            if (int.Parse(btn.name) >= DeckManager.Instance.Decks.Count)
                return;

            if (!previewDeckPanel.gameObject.activeSelf)
                previewDeckPanel.gameObject.SetActive(true);

            previewDeckPanel.SetPreviewDeck(DeckManager.Instance.Decks[int.Parse(btn.name)]);
        }

        public void OnAddCardClick(AddCardButton addCardButton)
        {
            previewDeckPanel.AddUnitCardToPreviewDeck(addCardButton.Card);
        }

        public void OnDeckViewClick()
        {
            previewDeckPanel.ChangeView();
        }

        #endregion

        #region Public Methods

        public void ChangeInteractableStatus(bool status)
        {
            closeButton.interactable = status;
            addDeckButton.interactable = status;

            deckViewButton.interactable = status;

            foreach (var t in deckButtons)
            {
                t.interactable = status;
            }

            foreach (var addButton in addCardButtons)
            {
                addButton.Button.interactable = status;
            }

            if (previewDeckPanel.PreviewDeck != null)
            {
                previewDeckPanel.gameObject.SetActive(status);
            }
        }

        /// <summary>
        /// Switches the current view.
        /// </summary>
        public void SwitchView()
        {
            cardView.SetActive(!cardView.activeSelf);
            deckView.SetActive(!cardView.activeSelf);
        }

        #endregion
    }
}