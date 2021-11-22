using System;
using GamePlay.GameManager.Scripts;
using SkillSystem.SkillTree.Scripts;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.In_Game.Building.Scripts
{
    public class BuildHUD : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button startWaveButton;

        public event Action<GameState> OnWaveStart;

        #region Unity Methods

        private void Awake()
        {
            GameManager.Instance.OnGameStateChanged += ChangeHUD;
            GameManager.Instance.PlayerModel.OnTryUpgradeSkill += OpenUpgradePanel;
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
            GameManager.Instance.OnGameStateChanged -= ChangeHUD;
        }

        #endregion

        #region Public Methods

        public void ChangeInteractableStatus(bool status)
        {
            startWaveButton.interactable = status;
        }

        public void OnClickStartWave()
        {
            OnWaveStart?.Invoke(GameState.Prepare);
            Debug.Log("Start");
        }

        #endregion

        #region Private Methods

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

        private void OpenUpgradePanel(SkillTree skillTree, Sprite img)
        {
            InGameCanvasManager.Scripts.InGameCanvasManager.Instance.UpgradePanel.gameObject.SetActive(true);
            InGameCanvasManager.Scripts.InGameCanvasManager.Instance.UpgradePanel.UpdateSkillTree(skillTree, img);
        }

        #endregion
    }
}