using _Project.UI.Managers.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.In_Game.Scripts
{
    public class HUD : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button settings;
        [SerializeField] private TextMeshProUGUI hqHealth;
        [SerializeField] private TextMeshProUGUI money;
        [SerializeField] private TextMeshProUGUI waveCount;

        private void OnEnable()
        {
            //CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            //CanvasManager.Instance.Unsubscribe(this);
        }
        
        public void ChangeInteractableStatus(bool status)
        {
            settings.interactable = status;
        }

        public void OnClickSettings()
        {
            
        }
    }
}