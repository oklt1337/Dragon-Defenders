using GamePlay.GameManager.Scripts;
using GamePlay.Player.PlayerModel.Scripts;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Utility.SceneManager.Scripts;

namespace UI.In_Game.Settings.Scripts
{
    public class InGameSettingsScreen : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button closeSettingsButton;
        [SerializeField] private Button toLobbyButton;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider musicSlider;

        #region Unity Methods

        private void OnEnable()
        {
            CanvasManager.Instance.Subscribe(this);
            GameManager.Instance.PlayerModel.ChangeState(State.Blocked);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
            GameManager.Instance.PlayerModel.ChangeState(State.Idle);
        }

        #endregion

        #region Public Methods

        public void ChangeInteractableStatus(bool status)
        {
            closeSettingsButton.interactable = status;
            toLobbyButton.interactable = status;
        }

        public void OnCloseSettingsClick()
        {
            gameObject.SetActive(false);
        }

        public void OnBackClick()
        {
            // Only for Prototype
            SceneManager.ChangeScene(Scene.MainMenu);
        }
        
        public void OnSfxChange()
        {
            AudioManager.Scripts.AudioManager.Instance.SetSfxVolume(sfxSlider.value);
        }
        
        public void OnMusicChange()
        {
            AudioManager.Scripts.AudioManager.Instance.SetMusicVolume(musicSlider.value);
        }

        #endregion
    }
}