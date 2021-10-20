using System;
using _Project.UI.MainMenu.Home_Screen.Scripts;
using UnityEngine;

namespace _Project.UI.MainMenu.Manager.Scripts
{
    public class MainMenuCanvasManager : MonoBehaviour
    {
        public static MainMenuCanvasManager Instance;
        
        [SerializeField] private HomeScreen homeScreen;

        public HomeScreen HomeScreen => homeScreen;

        private void Awake()
        {
            Instance = this;
        }
    }
}
