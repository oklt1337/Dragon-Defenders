using System;
using GamePlay.GameManager.Scripts;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI commanderHealthText;
        [SerializeField] private TextMeshProUGUI commanderManaText;

        #region Unity Methods

        private void Awake()
        {
            GameManager.Instance.OnGameStateChanged += ChangeHUD;
            GameManager.Instance.PlayerModel.Commander.OnCommanderHealthChanged += ChangeCommanderHealth;
            //GameManager.Instance.PlayerModel.Commander.OnCommanderManaChanged += ChangeCommanderMana;
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
            GameManager.Instance.PlayerModel.Commander.OnCommanderHealthChanged -= ChangeCommanderHealth;
            //GameManager.Instance.PlayerModel.Commander.OnCommanderManaChanged += ChangeCommanderMana;
        }

        #endregion


        public void ChangeInteractableStatus(bool status)
        {
            foreach (var ability in abilities)
            {
                ability.interactable = status;
            }
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

        /// <summary>
        /// Changes the Health of the Commander.
        /// </summary>
        private void ChangeCommanderHealth(float newHealth)
        {
            commanderHealth.value = newHealth;
            commanderHealthText.text = string.Concat((int)newHealth, "/" ,commanderHealth.maxValue);
        }

        /// <summary>
        /// Modifies the slider of the commander health.
        /// </summary>
        /// <param name="newMaxHealth"> The commanders new max health. </param>
        /// <param name="newHealth"> The commanders health value. </param>
        private void ModifyHealth(int newMaxHealth, float newHealth)
        {
            commanderHealth.maxValue = newMaxHealth;
            commanderHealth.value = newHealth;
            commanderHealthText.text = string.Concat((int)newHealth,"/" ,commanderHealth.maxValue);
        }
        
        /// <summary>
        /// Changes the Health of the Commander.
        /// </summary>
        private void ChangeCommanderMana(float newMana)
        {
            commanderMana.value = newMana;
            commanderManaText.text = string.Concat((int)newMana,"/" ,commanderMana.maxValue);
        }

        /// <summary>
        /// Modifies the slider of the commander health.
        /// </summary>
        /// <param name="newMaxMana"> The commanders new max mana. </param>
        /// <param name="newMana"> The commanders new mana value. </param>
        private void ModifyMana(int newMaxMana, float newMana)
        {
            commanderMana.maxValue = newMaxMana;
            commanderMana.value = newMana;
            commanderManaText.text = string.Concat((int)newMana,"/" ,commanderMana.maxValue);
        }
    }
}