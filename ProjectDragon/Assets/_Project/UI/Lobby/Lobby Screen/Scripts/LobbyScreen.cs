using _Project.UI.Lobby.Manager.Scripts;
using Deck_Cards.DeckManager.Scripts;
using Photon.Pun;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Utility.SceneManager.Scripts;

namespace UI.Lobby.Lobby_Screen.Scripts
{
    public class LobbyScreen : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button mapButton;

        public void ChangeInteractableStatus(bool status)
        {
            mapButton.interactable = status;
        }

        public void OnMapClick()
        {
            if(!CheckDeck())
                return;
            
            SceneManager.ChangeScene(Scene.GameScene);
        }

        public void OnBackClick()
        {
            SceneManager.ChangeScene(Scene.MainMenu);
        }

        public void OnDeckManagerClick()
        {
            LobbyCanvasManager.Instance.DeckManagerScreen.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Checks if the deck is legal.
        /// </summary>
        /// <returns></returns>
        private bool CheckDeck()
        {
            var hashTable = PhotonNetwork.LocalPlayer.CustomProperties;

            if (!hashTable.ContainsKey("PlayDeck")) 
                return false;
            
            int deckId = (int) hashTable["PlayDeck"];
                
            // Checks if deck exists.
            if (DeckManager.Instance.Decks.Count <= deckId)
            {
                return false;
            }

            var deck = DeckManager.Instance.Decks[deckId];
                
            // Checks if deck is legal.
            return deck.IsUseAble;
        }
    }
}