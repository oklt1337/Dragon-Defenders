using System;
using GamePlay.GameManager.Scripts;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.In_Game.Commander.Scripts
{
    public class CommanderHUD : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button[] abilities;
        [SerializeField] private Slider commanderHealth;
        [SerializeField] private Slider commanderMana;

        #region Unity Methods

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

        #endregion


        public void ChangeInteractableStatus(bool status)
        {
            foreach (var t in abilities)
            {
                t.interactable = status;
            }

            commanderHealth.interactable = status;
            commanderMana.interactable = status;
        }

        private void ChangeHUD(GameState state)
        {
            switch (state)
            {
                case GameState.Prepare:
                    gameObject.SetActive(false);
                    break;
                case GameState.Build:
                    gameObject.SetActive(false);
                    break;
                case GameState.Wave:
                    gameObject.SetActive(true);
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