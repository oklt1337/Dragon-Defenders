using _Project.UI.MainMenu.Deck_Manager_Screen.Scripts;
using _Project.UI.MainMenu.Egg_Hatching_Screen.Scripts;
using _Project.UI.MainMenu.Friend_List_Screen.Scripts;
using _Project.UI.MainMenu.Home_Screen.Scripts;
using _Project.UI.MainMenu.Notification_Screen.Scripts;
using _Project.UI.MainMenu.Profile_Screen.Scripts;
using _Project.UI.MainMenu.Settings_Screen.Scripts;
using _Project.UI.MainMenu.Shop_Screen.Scripts;
using UnityEngine;

namespace _Project.UI.MainMenu.Manager.Scripts
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
        [SerializeField] private DeckManagerScreen deckManagerScreen;

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

        #endregion

        private void Awake()
        {
            Instance = this;
        }
    }
}
