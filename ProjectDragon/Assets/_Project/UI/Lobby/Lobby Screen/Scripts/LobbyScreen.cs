using _Project.UI.Lobby.Manager.Scripts;
using Deck_Cards.DeckManager.Scripts;
using GamePlay.GameManager.Scripts;
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
            if(!GameManager.DefaultDeck.IsUseAble)
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
    }
}