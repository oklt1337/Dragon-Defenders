using System.Collections.Generic;
using Deck_Cards.DeckManager.Scripts;
using Deck_Cards.Decks.Scripts;
using UI.MainMenu.Manager.Scripts;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Utility.SceneManager.Scripts;

namespace UI.Deck_Preview.Scripts
{
    public class PreviewDeckPanel : MonoBehaviour, ICanvas
    {
        [Header("Base View")]
        [SerializeField] private InputField deckName;
        [SerializeField] private Button commanderButton;
        [SerializeField] private List<Button> unitButtons;

        [Header("Deck View")] 
        [SerializeField] private Button editDeckButton;
        [SerializeField] private Button setDefaultButton;

        [Header("Card View")] 
        [SerializeField] private Button deleteDeckButton;
        [SerializeField] private Button saveDeckButton;

        public Deck PreviewDeck { get; private set; }

        /// <summary>
        /// Sets the Preview Deck.
        /// </summary>
        /// <param name="newPreviewDeck"> The deck you want previewed. </param>
        public void SetPreviewDeck(Deck newPreviewDeck)
        {
            PreviewDeck = newPreviewDeck;
            AdjustPictures();
        }

        public void OnEditDeckClick()
        { 
            ChangeView();
        }

        /// <summary>
        /// Sets the Deck as default.
        /// </summary>
        public void OnSetDefaultClick()
        {
            DeckManager.Instance.SetDeckAsDefault(PreviewDeck.DeckId);
        }

        public void OnSaveDeckClick()
        {
            DeckManager.Instance.EditDeckName(PreviewDeck, deckName.text);
            
            // Future TODO:
        }

        public void OnDeleteClick()
        {
            if(DeckManager.Instance.Decks.Count < 2)
                return;
            
            DeckManager.Instance.DeleteDeck(PreviewDeck.DeckId);

            ChangeView();
            
            gameObject.SetActive(false);
            
        }

        /// <summary>
        /// Sets the pictures of the Buttons correctly.
        /// </summary>
        private void AdjustPictures()
        {
            commanderButton.image.sprite = PreviewDeck.CommanderCard.Icon;

            for (int i = 0; i < PreviewDeck.UnitCards.Count; i++)
            {
                unitButtons[i].image.sprite = PreviewDeck.UnitCards[i].Icon;
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
                button.interactable = status;
            }
        }
    }
}