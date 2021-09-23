using System; 
using _Project.Utility.DeveloperConsole.Scripts.Commands;
using TMPro;
using UnityEngine;

namespace _Project.Utility.DeveloperConsole.Scripts
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

        private float _pausedTimeScale;

        private DeveloperConsole _developerConsole;

        #endregion

        #region Protected Fields

        

        #endregion

        #region Public Fields

        

        #endregion

        #region Properties

        private DeveloperConsole DeveloperConsole
        {
            get
            {
                if (_developerConsole != null)
                    return _developerConsole;

                return _developerConsole = new DeveloperConsole(prefix, commands);
            }
        }

        #endregion

        #region Events

        

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

        private void Toggle()
        {
            if (console.activeSelf)
            {
                Time.timeScale = _pausedTimeScale;
                console.SetActive(false);
            }
            else
            {
                _pausedTimeScale = Time.timeScale;
                Time.timeScale = 0;
                console.SetActive(true);
                inputField.ActivateInputField();
            }
        }

        #endregion

        #region Public Methods

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
