using System;
using _Project.GamePlay.GameManager.Scripts;
using _Project.SkillSystem.SkillTree;
using _Project.UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.In_Game.Scripts
{
    public class BuildHUD : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button startWaveButton;
        [SerializeField] private UpgradePanel upgradePanel;

        public event Action<GameState> OnWaveStart;

        private void Awake()
        {
            GameManager.Instance.OnGameStateChanged += ChangeHUD;
            GameManager.Instance.PlayerModel.OnTryUpgradeSkill += OpenUpgradePanel;
        }

        private void OnEnable()
        {
            //CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            //CanvasManager.Instance.Unsubscribe(this);
        }
        
        private void OnDestroy()
        {
            GameManager.Instance.OnGameStateChanged -= ChangeHUD;
        }
        
        public void ChangeInteractableStatus(bool status)
        {
            startWaveButton.interactable = status;
        }

        public void OnClickStartWave()
        {
            OnWaveStart?.Invoke(GameState.Prepare);
            Debug.Log("Start");
        }

        private void ChangeHUD(GameState state)
        {
            switch (state)
            {
                case GameState.Prepare:
                    gameObject.SetActive(false);
                    break;
                case GameState.Build:
                    gameObject.SetActive(true);
                    break;
                case GameState.Wave:
                    gameObject.SetActive(false);
                    break;
                case GameState.End:
                    gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OpenUpgradePanel(SkillTree skillTree)
        {
            upgradePanel.gameObject.SetActive(true);
            upgradePanel.UpdateSkillTree(skillTree);
        }
    }
}