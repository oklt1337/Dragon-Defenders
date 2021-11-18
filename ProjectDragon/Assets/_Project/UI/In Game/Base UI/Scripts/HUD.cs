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

        private void Start()
        {
            OnWaveChange();
            OnMoneyChange();
            OnHqHealthChange();
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
            InGameCanvasManager.Scripts.InGameCanvasManager.Instance.InGameSettingsScreen.gameObject.SetActive(true);
        }

        /// <summary>
        /// Changes the visible Hq Health. 
        /// </summary>
        public void OnHqHealthChange()
        {
            //hqHealth.text = GameManager.Instance.Hq.Hq.Health.ToString();
        }
        
        /// <summary>
        /// Changes the visible money. 
        /// </summary>
        public void OnMoneyChange()
        {
            money.text = GameManager.Instance.PlayerModel.Money.ToString();
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