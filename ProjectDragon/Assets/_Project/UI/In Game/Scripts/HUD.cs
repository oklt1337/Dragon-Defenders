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

        public void ChangeInteractableStatus(bool status)
        {
            settings.interactable = status;
        }

        public void OnClickSettings()
        {
            
        }

        public void ChangeActiveHUD()
        {
            
        }
    }
}
