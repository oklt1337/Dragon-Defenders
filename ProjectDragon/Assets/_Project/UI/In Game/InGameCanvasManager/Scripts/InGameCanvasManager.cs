using _Project.UI.In_Game.Commander.Scripts;
using UI.In_Game.Base_UI.Scripts;
using UI.In_Game.Building.Scripts;
using UnityEngine;

namespace UI.In_Game.InGameCanvasManager.Scripts
{
    public class InGameCanvasManager : MonoBehaviour
    {
        public static InGameCanvasManager Instance;

        #region Private Fields

        [SerializeField] private HUD hud;
        [SerializeField] private BuildHUD buildHUD;
        [SerializeField] private TowerSpawnButton[] towerSpawnButton;
        [SerializeField] private UpgradePanel upgradePanel;
        [SerializeField] private CommanderHUD commanderHUD;
        [SerializeField] private InGameSettingsScreen inGameSettingsScreen;

        #endregion

        #region Public Properties

        public HUD HUD => hud;
        public BuildHUD BuildHUD => buildHUD;
        public TowerSpawnButton[] TowerSpawnButton => towerSpawnButton;
        public UpgradePanel UpgradePanel => upgradePanel;
        public CommanderHUD CommanderHUD => commanderHUD;
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