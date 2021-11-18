using System;
using Photon.Pun;
using UI.MainMenu.Manager.Scripts;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu.Settings_Screen.Scripts
{
    public class MainMenuSettingsScreen : MonoBehaviourPun, ICanvas
    {
        [SerializeField] private Button logOutButton;
        [SerializeField] private Button homeScreenButton;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider musicSlider;

        #region Events

        public static event Action OnLogout;

        #endregion
        
        #region Unity Methods

        private void OnEnable()
        {
            CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
        }

        #endregion
        
        public void OnClickLogOut()
        {
            OnLogout?.Invoke();
        }

        public void OnClickHomeScreen()
        {
            MainMenuCanvasManager.Instance.HomeScreen.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnSfxChange()
        {
            AudioManager.Scripts.AudioManager.Instance.SetSfxVolume(sfxSlider.value);
        }
        
        public void OnMusicChange()
        {
            AudioManager.Scripts.AudioManager.Instance.SetMusicVolume(musicSlider.value);
        }

        public void ChangeInteractableStatus(bool status)
        {
            logOutButton.interactable = status;
            homeScreenButton.interactable = status;
            sfxSlider.interactable = status;
            musicSlider.interactable = status;
        }
    }
}