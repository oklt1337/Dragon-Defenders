using System;
using _Project.UI.Lobby.Manager.Scripts;
using _Project.UI.MainMenu.Manager.Scripts;
using _Project.UI.Managers.Scripts;
using _Project.Utility.SceneManager.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.Deck_Manager_Screen.Scripts
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
            switch (SceneManager.CurrentScene)
            {
                case Scene.Intro:
                    break;
                case Scene.Authorize:
                    break;
                case Scene.MainMenu:
                    MainMenuCanvasManager.Instance.HomeScreen.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    break;
                case Scene.Lobby:
                    LobbyCanvasManager.Instance.LobbyScreen.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    break;
                case Scene.GameScene:
                    break;
            }
        }

        /// <summary>
        /// Opens the new deck screen and turns itself off.
        /// </summary>
        public void OnAddClick()
        {
            switch (SceneManager.CurrentScene)
            {
                case Scene.Intro:
                    break;
                case Scene.Authorize:
                    break;
                case Scene.MainMenu:
                    MainMenuCanvasManager.Instance.NewDeckScreen.gameObject.SetActive(true);
                    ChangeInteractableStatus(false);
                    break;
                case Scene.Lobby:
                    LobbyCanvasManager.Instance.NewDeckScreen.gameObject.SetActive(true);
                    ChangeInteractableStatus(false);
                    break;
                case Scene.GameScene:
                    break;
            } 
        }

        public void ChangeInteractableStatus(bool status)
        {
            closeButton.interactable = status;
            addDeckButton.interactable = status;
        }
    }
}