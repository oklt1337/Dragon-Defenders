using System.Globalization;
using GamePlay.GameManager.Scripts;
using TMPro;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.In_Game.Base_UI.Scripts
{
    public class HUD : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button settings;
        [SerializeField] private TextMeshProUGUI hqHealth;
        [SerializeField] private TextMeshProUGUI money;
        [SerializeField] private TextMeshProUGUI waveCount;

        [Header("Game Over")] 
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private TextMeshProUGUI waveScore;
        [SerializeField] private TextMeshProUGUI highScore;

        #region Unity Methods

        private void Awake()
        {
            OnWaveChange();
            GameManager.Instance.PlayerModel.OnPlayerMoneyChanged += OnMoneyChange;
            GameManager.Instance.Hq.Hq.OnHqHealthChanged += OnHqHealthChange;
            GameManager.Instance.Hq.Hq.OnDeath += GameOver;
            GameManager.Instance.PlayerModel.Commander.CommanderStats.OnCommanderDeath += GameOver;
        }

        private void OnEnable()
        {
            CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
        }

        private void OnDestroy()
        {
            GameManager.Instance.PlayerModel.OnPlayerMoneyChanged -= OnMoneyChange;
            GameManager.Instance.Hq.Hq.OnHqHealthChanged -= OnHqHealthChange;
            GameManager.Instance.Hq.Hq.OnDeath -= GameOver;
            GameManager.Instance.PlayerModel.Commander.CommanderStats.OnCommanderDeath -= GameOver; 
        }

        #endregion


        public void ChangeInteractableStatus(bool status)
        {
            settings.interactable = status;
        }

        public void OnClickSettings()
        {
            // Make sure the Upgrade Panel is closed during the settings.
            InGameCanvasManager.Scripts.InGameCanvasManager.Instance.UpgradePanel.Close();
            InGameCanvasManager.Scripts.InGameCanvasManager.Instance.InGameSettingsScreen.gameObject.SetActive(true);
        }

        /// <summary>
        /// Changes the visible wave count. 
        /// </summary>
        public void OnWaveChange()
        {
            waveCount.text = GameManager.Instance.WaveManager.CurrentWaveIndex.ToString();
        }

        /// <summary>
        /// Changes the visible Hq Health. 
        /// </summary>
        private void OnHqHealthChange(float newHealth)
        {
            hqHealth.text = newHealth.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Changes the visible money. 
        /// </summary>
        private void OnMoneyChange(float newMoney)
        {
            money.text = newMoney.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Shows the Game Over to the player.
        /// </summary>
        private void GameOver()
        {
            gameOverScreen.SetActive(true);
            InGameCanvasManager.Scripts.InGameCanvasManager.Instance.BuildHUD.gameObject.SetActive(false);
            InGameCanvasManager.Scripts.InGameCanvasManager.Instance.CommanderHUD.gameObject.SetActive(false);
            settings.gameObject.SetActive(false);

            waveScore.text = string.Concat("You Reached Wave: ", GameManager.Instance.WaveManager.CurrentWaveIndex.ToString());
            highScore.text = string.Concat("Your Score was:\n", GameManager.Instance.WaveManager.CurrentWaveIndex.ToString());
        }
    }
}