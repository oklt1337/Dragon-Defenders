using _Project.UI.Lobby.Lobby_Screen.Scripts;
using UnityEngine;

namespace _Project.UI.Lobby.Manager.Scripts
{
    public class LobbyCanvasManager : MonoBehaviour
    {
        public static LobbyCanvasManager Instance;

        [SerializeField] private LobbyScreen lobbyScreen;

        public LobbyScreen LobbyScreen => lobbyScreen;
        
        private void Awake()
        {
            Instance = this;
        }
    }
}
