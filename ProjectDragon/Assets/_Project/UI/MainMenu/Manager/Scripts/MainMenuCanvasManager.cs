using UI.Decks_Cards.Deck_Manager_Screen.Scripts;
using UI.Decks_Cards.New_Deck_Screen.Scripts;
using UI.MainMenu.Egg_Hatching_Screen.Scripts;
using UI.MainMenu.Friend_List_Screen.Scripts;
using UI.MainMenu.Home_Screen.Scripts;
using UI.MainMenu.Notification_Screen.Scripts;
using UI.MainMenu.Profile_Screen.Scripts;
using UI.MainMenu.Settings_Screen.Scripts;
using UI.MainMenu.Shop_Screen.Scripts;
using UnityEngine;

namespace UI.MainMenu.Manager.Scripts
{
    public class MainMenuCanvasManager : MonoBehaviour
    {
        public static MainMenuCanvasManager Instance;

        #region Serialized Fields
        
        [SerializeField] private HomeScreen homeScreen;
        [SerializeField] private ShopScreen shopScreen;
        [SerializeField] private MainMenuSettingsScreen mainMenuSettingsScreen;
        [SerializeField] private ProfileScreen profileScreen;
        [SerializeField] private NotificationScreen notificationScreen;
        [SerializeField] private FriendListScreen friendListScreen;
        [SerializeField] private EggHatchingScreen eggHatchingScreen;
        
        [Header("Decks and Cards related Screens")]
        [SerializeField] private DeckManagerScreen deckManagerScreen;
        [SerializeField] private NewDeckScreen newDeckScreen;

        #endregion

        #region Properties 
        
        public HomeScreen HomeScreen => homeScreen;
        public ShopScreen ShopScreen => shopScreen;
        public MainMenuSettingsScreen MainMenuSettingsScreen => mainMenuSettingsScreen;
        public ProfileScreen ProfileScreen => profileScreen;
        public NotificationScreen NotificationScreen => notificationScreen;
        public FriendListScreen FriendListScreen => friendListScreen;
        public EggHatchingScreen EggHatchingScreen => eggHatchingScreen;
        public DeckManagerScreen DeckManagerScreen => deckManagerScreen;
        public NewDeckScreen NewDeckScreen => newDeckScreen;

        #endregion

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
