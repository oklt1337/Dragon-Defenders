using UI.Deck_Manager_Screen.Scripts;
using UI.Lobby.Lobby_Screen.Scripts;
using UI.New_Deck_Screen.Scripts;
using UnityEngine;

namespace _Project.UI.Lobby.Manager.Scripts
{
    public class LobbyCanvasManager : MonoBehaviour
    {
        public static LobbyCanvasManager Instance;

        [SerializeField] private LobbyScreen lobbyScreen;
        
        [Header("Decks and Cards related Screens")]
        [SerializeField] private DeckManagerScreen deckManagerScreen;
        [SerializeField] private NewDeckScreen newDeckScreen;

        public LobbyScreen LobbyScreen => lobbyScreen;
        public DeckManagerScreen DeckManagerScreen => deckManagerScreen;
        public NewDeckScreen NewDeckScreen => newDeckScreen;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
    }
}