using System;
using TMPro;
using UnityEngine;
using Utility.DeveloperConsole.Scripts.Commands;

namespace Utility.DeveloperConsole.Scripts
{
    public class DeveloperConsoleManager : MonoBehaviour
    {
        private static DeveloperConsoleManager Instance;
        
        #region SerialzeFields

        [SerializeField] private string prefix = string.Empty;
        [SerializeField] private ConsoleCommand[] commands = Array.Empty<ConsoleCommand>();

        [Header("UI")] 
        [SerializeField] private GameObject console;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TextMeshProUGUI debugLog;

        #endregion

        #region Private Fields

        private float pausedTimeScale;
        private DeveloperConsole developerConsole;

        #endregion

        #region Properties

        private DeveloperConsole DeveloperConsole
        {
            get
            {
                if (developerConsole != null)
                    return developerConsole;

                return developerConsole = new DeveloperConsole(prefix, commands);
            }
        }

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.BackQuote)) 
                return;

            Toggle();
        }

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Toggle console on and off
        /// </summary>
        private void Toggle()
        {
            if (console.activeSelf)
            {
                Time.timeScale = pausedTimeScale;
                console.SetActive(false);
            }
            else
            {
                pausedTimeScale = Time.timeScale;
                Time.timeScale = 0;
                console.SetActive(true);
                inputField.ActivateInputField();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Process Input of player and pars it to console.
        /// </summary>
        /// <param name="inputText">string</param>
        public void ProcessCommand(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
                return;

            string trimmedInput = inputText.TrimStart();
            debugLog.text += string.IsNullOrEmpty(debugLog.text) ? trimmedInput : $"\n{trimmedInput}";
            
            DeveloperConsole.ProcessCommand(trimmedInput);

            inputField.text = string.Empty;
        }

        #endregion
    }
}
