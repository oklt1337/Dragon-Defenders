using UI.In_Game.Base_UI.Scripts;
using UI.In_Game.Building.Scripts;
using UI.In_Game.Commander.Scripts;
using UI.In_Game.Settings.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.In_Game.InGameCanvasManager.Scripts
{
    public class InGameCanvasManager : MonoBehaviour
    {
        public static InGameCanvasManager Instance;

        #region Private Fields
        
        [SerializeField] private HUD hud;
        
        [Header("Wave UI")]
        [SerializeField] private CommanderHUD commanderHUD;
        
        [Header("Build UI")]
        [SerializeField] private BuildHUD buildHUD;
        [SerializeField] private UpgradePanel upgradePanel;
        [SerializeField] private InGameSettingsScreen inGameSettingsScreen;

        #endregion

        #region Public Properties

        public HUD HUD => hud;
        
        public CommanderHUD CommanderHUD => commanderHUD;
        
        public BuildHUD BuildHUD => buildHUD;
        public UpgradePanel UpgradePanel => upgradePanel;
        public InGameSettingsScreen InGameSettingsScreen => inGameSettingsScreen;

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