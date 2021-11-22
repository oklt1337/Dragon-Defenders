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

        private void Awake()
        {
            OnWaveChange();
            GameManager.Instance.PlayerModel.OnPlayerMoneyChanged += OnMoneyChange;
            GameManager.Instance.Hq.Hq.OnHqHealthChanged += OnHqHealthChange;
        }

        private void OnEnable()
        {
            CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
        }
        
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
        /// Changes the visible wave count. 
        /// </summary>
        public void OnWaveChange()
        {
            waveCount.text = GameManager.Instance.WaveManager.CurrentWaveIndex.ToString();
        }
    }
}