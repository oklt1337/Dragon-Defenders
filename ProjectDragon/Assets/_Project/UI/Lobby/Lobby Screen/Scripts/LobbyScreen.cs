using _Project.UI.Managers.Scripts;
using _Project.Utility.SceneManager.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.Lobby.Lobby_Screen.Scripts
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
            SceneManager.ChangeScene(Scene.GameScene);
        }

        public void OnBackClick()
        {
            SceneManager.ChangeScene(Scene.MainMenu);
        }
    }
}
