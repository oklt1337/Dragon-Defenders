using System.Collections.Generic;
using System.Linq;
using Deck_Cards.Cards.BaseCards.Scripts;
using Deck_Cards.Cards.CommanderCard.Scripts;
using Deck_Cards.DeckManager.Scripts;
using Deck_Cards.Decks.Scripts;
using TMPro;
using UI.MainMenu.Manager.Scripts;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Utility.SceneManager.Scripts;
using ICanvas = UI.Managers.Scripts.ICanvas;

namespace UI.Deck_Preview.Scripts
{
    public class PreviewDeckPanel : MonoBehaviour, ICanvas
    {
        [Header("Base View")] 
        [SerializeField] private TMP_InputField deckName;
        [SerializeField] private Button commanderButton;
        [SerializeField] private List<PreviewDeckButton> unitButtons;

        [Header("Deck View")] 
        [SerializeField] private Button editDeckButton;
        [SerializeField] private Button setDefaultButton;

        [Header("Card View")] 
        [SerializeField] private Button deleteDeckButton;
        [SerializeField] private Button saveDeckButton;

        public Deck PreviewDeck { get; private set; }

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

        #region Button Methods

        public void OnEditDeckClick()
        {
            ChangeView();
            AudioManager.Scripts.AudioManager.Instance.PlayAudioClip(AudioManager.Scripts.AudioManager.Instance.UiSound[1]);
        }

        /// <summary>
        /// Sets the Deck as default.
        /// </summary>
        public void OnSetDefaultClick()
        {
            if (PreviewDeck.UnitCards.Contains(null))
                return;

            if (PreviewDeck.CommanderCard == null)
                return;

            DeckManager.Instance.SetDeckAsDefault(PreviewDeck.DeckId);
            AudioManager.Scripts.AudioManager.Instance.PlayAudioClip(AudioManager.Scripts.AudioManager.Instance.UiSound[1]);
        }

        public void OnSaveDeckClick()
        {
            DeckManager.Instance.EditDeckName(PreviewDeck, deckName.text);

            if (SceneManager.CurrentScene == Scene.MainMenu)
            {
                MainMenuCanvasManager.Instance.DeckManagerScreen.UpdateDecks();
            }
            else
            {
                MainMenuCanvasManager.Instance.DeckManagerScreen.UpdateDecks();
            }

            DeckManager.SaveDeck(PreviewDeck);
            AudioManager.Scripts.AudioManager.Instance.PlayAudioClip(AudioManager.Scripts.AudioManager.Instance.UiSound[1]);
            ChangeView();
        }

        public void OnDeleteClick()
        {
            if (DeckManager.Instance.Decks.Count < 2)
                return;

            DeckManager.Instance.DeleteDeck(PreviewDeck.DeckId);

            if (SceneManager.CurrentScene == Scene.MainMenu)
            {
                MainMenuCanvasManager.Instance.DeckManagerScreen.UpdateDecks();
            }
            else
            {
                MainMenuCanvasManager.Instance.DeckManagerScreen.UpdateDecks();
            }

            ChangeView();

            gameObject.SetActive(false);
            AudioManager.Scripts.AudioManager.Instance.PlayAudioClip(AudioManager.Scripts.AudioManager.Instance.UiSound[1]);
        }

        public void OnRemoveCardClick(PreviewDeckButton deckButton)
        {
            if (editDeckButton.gameObject.activeSelf)
                return;

            if (deckButton.Card == null)
                return;

            if (deckButton.Card is CommanderCard)
                PreviewDeck.RemoveCard(deckButton.Card);
            else
            {
                PreviewDeck.RemoveCard(deckButton.Card, int.Parse(deckButton.name));
            }

            deckButton.SetCard(null);
            AudioManager.Scripts.AudioManager.Instance.PlayAudioClip(AudioManager.Scripts.AudioManager.Instance.UiSound[2]);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the Preview Deck.
        /// </summary>
        /// <param name="newPreviewDeck"> The deck you want previewed. </param>
        public void SetPreviewDeck(Deck newPreviewDeck)
        {
            PreviewDeck = newPreviewDeck;

            if (newPreviewDeck != null)
                AdjustButtons();
        }

        /// <summary>
        /// Sets the the Buttons correctly.
        /// </summary>
        private void AdjustButtons()
        {
            deckName.text = PreviewDeck.DeckName;

            for (int i = 0; i < unitButtons.Count; i++)
            {
                unitButtons[i].SetCard(PreviewDeck.UnitCards[i]);
            }

            var text = commanderButton.GetComponentInChildren<TextMeshProUGUI>();
            if (PreviewDeck.CommanderCard == null)
            {
                text.text = "(Empty)";
            }
            else
            {
                text.text = PreviewDeck.CommanderCard.cardName;
                commanderButton.image.sprite = PreviewDeck.CommanderCard.Icon;
            }
        }

        /// <summary>
        /// Changes the view.
        /// </summary>
        public void ChangeView()
        {
            if (SceneManager.CurrentScene == Scene.MainMenu)
            {
                MainMenuCanvasManager.Instance.DeckManagerScreen.SwitchView();
            }
            else
            {
                MainMenuCanvasManager.Instance.DeckManagerScreen.SwitchView();
            }

            bool tempBool = editDeckButton.gameObject.activeSelf;

            editDeckButton.gameObject.SetActive(!tempBool);
            setDefaultButton.gameObject.SetActive(!tempBool);

            deleteDeckButton.gameObject.SetActive(tempBool);
            saveDeckButton.gameObject.SetActive(tempBool);
        }

        /// <summary>
        /// Adds a Unit Card to the preview deck if there is free space.
        /// </summary>
        /// <param name="addCard"> The card that shall be added. </param>
        public void AddUnitCardToPreviewDeck(BaseCard addCard)
        {
            for (int i = 0; i < PreviewDeck.UnitCards.Length; i++)
            {
                if (PreviewDeck.UnitCards[i] != null)
                    continue;

                // Visuals.
                if (!PreviewDeck.AddCard(addCard, i))
                    return;

                unitButtons[i].SetCard(addCard);
                unitButtons[i].Text.text = addCard.cardName;
                return;
            }
        }

        public void ChangeInteractableStatus(bool status)
        {
            deckName.interactable = status;
            commanderButton.interactable = status;

            editDeckButton.interactable = status;
            setDefaultButton.interactable = status;

            deleteDeckButton.interactable = status;
            saveDeckButton.interactable = status;

            foreach (var button in unitButtons)
            {
                button.Button.interactable = status;
            }
        }

        #endregion
    }
}