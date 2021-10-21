using _Project.UI.Lobby.Lobby_Screen.Scripts;
using _Project.UI.MainMenu.Deck_Manager_Screen.Scripts;
using UnityEngine;

namespace _Project.UI.Lobby.Manager.Scripts
{
    public class LobbyCanvasManager : MonoBehaviour
    {
        public static LobbyCanvasManager Instance;

        [SerializeField] private LobbyScreen lobbyScreen;
        [SerializeField] private DeckManagerScreen deckManagerScreen;

        public LobbyScreen LobbyScreen => lobbyScreen;
        public DeckManagerScreen DeckManagerScreen => deckManagerScreen;
        
        private void Awake()
        {
            Instance = this;
        }
    }
}