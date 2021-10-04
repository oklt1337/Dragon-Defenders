using System;
using _Project.GamePlay.GameManager.Scripts;
using _Project.UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.In_Game.Scripts
{
    public class BuildHUD : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button startWaveButton;

        public event Action<GameState> OnWaveStart;

        private void Awake()
        {
            GameManager.Instance.OnGameStateChanged += ChangeHUD;
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
        
        public void ChangeInteractableStatus(bool status)
        {
            startWaveButton.interactable = status;
        }

        public void OnClickStartWave()
        {
            OnWaveStart?.Invoke(GameState.Wave);
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
    }
}
