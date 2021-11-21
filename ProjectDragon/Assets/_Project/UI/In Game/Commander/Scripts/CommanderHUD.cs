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
        }

        private void Start()
        {
            var commanderStat = GameManager.Instance.PlayerModel.Commander.CommanderStats;
            commanderStat.OnCommanderHealthChanged += ChangeCommanderHealth;
            commanderStat.OnCommanderManaChanged += ChangeCommanderMana;
            commanderStat.OnCommanderMAXHealthChanged += ModifyHealth;
            commanderStat.OnCommanderMAXManaChanged += ModifyMana;
            
            for (int i = 0; i < abilities.Length; i++)
            {
                abilities[i].image.sprite = GameManager.Instance.PlayerModel.Commander.Abilities[i].Icon;
            }
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
            
            var commanderStat = GameManager.Instance.PlayerModel.Commander.CommanderStats;
            commanderStat.OnCommanderHealthChanged -= ChangeCommanderHealth;
            commanderStat.OnCommanderManaChanged -= ChangeCommanderMana;
            commanderStat.OnCommanderMAXHealthChanged -= ModifyHealth;
            commanderStat.OnCommanderMAXManaChanged -= ModifyMana;
        }

        #endregion

        #region Public Methods

        public void CastSpellOne()
        {
            GameManager.Instance.PlayerModel.Commander.Attack1(null);
        }
        
        public void CastSpellTwo()
        {
            GameManager.Instance.PlayerModel.Commander.Attack2(null);
        }
        
        public void CastSpellThree()
        {
            GameManager.Instance.PlayerModel.Commander.Attack3(null);
        }
        
        public void ChangeInteractableStatus(bool status)
        {
            foreach (var ability in abilities)
            {
                ability.interactable = status;
            }
        }

        #endregion


        #region Private Methods

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
        private void ModifyHealth(float newMaxHealth)
        {
            commanderHealth.maxValue = newMaxHealth;
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
        private void ModifyMana(float newMaxMana)
        {
            commanderMana.maxValue = newMaxMana;
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
        
        #endregion
        
    }
}