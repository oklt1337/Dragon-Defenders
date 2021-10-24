using System;
using _Project.Network.NetworkManager.Scripts;
using _Project.Network.PlayFab.Scripts;
using _Project.UI.Managers.Scripts;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.Authorize.Scripts
{
    public class AuthorizeCanvas : MonoBehaviour, ICanvas
    {
        #region SerialzeFields

        [Header("Login")]
        [SerializeField] private TMP_InputField userName;
        [SerializeField] private TMP_InputField password;
        [SerializeField] private Button loginButton;

        [Header("Register")]
        [SerializeField] private TMP_InputField confirmPassword;
        [SerializeField] private Button registerButton;
        [SerializeField] private Button cancelRegisterButton;

        [Header("Socials")] 
        [SerializeField] private Button loginFacebookButton;
        [SerializeField] private Button loginGoogleButton;

        [Header("LowerPanel")] 
        [SerializeField] private Toggle rememberMe;
        [SerializeField] private Button optionsButton;

        [Header("Panels")]
        [SerializeField] private GameObject registerPanel;
        [SerializeField] private GameObject optionsPanel;

        #endregion

        #region Events

        public static event Action<string, string, AuthTypes> OnLogin;
        public static event Action<string, string, AuthTypes> OnRegister;
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            NetworkManager.Instance.PlayFabManager.PlayFabAuthManager.OnAccountNotFound += OpenRegisterPanel;
        }

        public void Start()
        {
            //Bind auth remember me status to toggle.
            rememberMe.onValueChanged.AddListener((arg0 => PlayFabAuthService.RememberMe = arg0));

            //Bind to UI buttons to perform actions when user interacts with the UI.
            loginButton.onClick.AddListener(OnClickLogin);
            loginFacebookButton.onClick.AddListener(OnClickLoginWithFacebook);
            loginGoogleButton.onClick.AddListener(OnClickLoginWithGoogle);
            registerButton.onClick.AddListener(OnClickRegister);
            cancelRegisterButton.onClick.AddListener(OnClickCancelRegister);
            optionsButton.onClick.AddListener(OnClickOptions);

            //Default is true
            rememberMe.isOn = true;
            PlayFabAuthService.RememberMe = rememberMe.isOn;
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
            NetworkManager.Instance.PlayFabManager.PlayFabAuthManager.OnAccountNotFound -= OpenRegisterPanel;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Opening Register Panel.
        /// </summary>
        private void OpenRegisterPanel()
        {
            registerPanel.SetActive(true);
            ChangeInteractableStatusForRegisterPanel(false);
        }

        /// <summary>
        /// Login with email / password combo
        /// </summary>
        private void OnClickLogin()
        {
            OnLogin?.Invoke(userName.text, password.text, AuthTypes.EmailAndPassword);
        }

        /// <summary>
        /// Register new user with email /  password combo.
        /// </summary>
        private void OnClickRegister()
        {
            if (password.text != confirmPassword.text)
            {
                Debug.Log("Passwords do not Match.");
                return;
            }
            
            OnRegister?.Invoke(userName.text, password.text, AuthTypes.RegisterPlayFabAccount);
        }

        /// <summary>
        /// Cancel the Registration process.
        /// </summary>
        private void OnClickCancelRegister()
        {
            //Reset all forms
            userName.text = string.Empty;
            password.text = string.Empty;
            confirmPassword.text = string.Empty;
            //Show panels
            registerPanel.SetActive(false);
            ChangeInteractableStatusForRegisterPanel(true);
        }

        /// <summary>
        /// Login with a facebook account.  This kicks off the request to facebook
        /// </summary>
        private void OnClickLoginWithFacebook()
        {
            Debug.Log("Logging In to Facebook..");
        }

        /// <summary>
        /// Login with a google account.  This kicks off the request to google play games.
        /// </summary>
        private void OnClickLoginWithGoogle()
        {
            Debug.Log("Logging In to Google..");
        }

        /// <summary>
        /// Activate Option panel.
        /// </summary>
        private void OnClickOptions()
        {
            optionsPanel.SetActive(true);
        }

        /// <summary>
        /// Sets interactable status of buttons of the loginPanel.
        /// </summary>
        /// <param name="status">bool</param>
        private void ChangeInteractableStatusForRegisterPanel(bool status)
        {
            userName.interactable = status;
            password.interactable = status;
            loginButton.interactable = status;

            loginFacebookButton.interactable = status;
            loginGoogleButton.interactable = status;
            
            rememberMe.interactable = status;
            optionsButton.interactable = status;
        }

        #endregion

        #region Public Methods

        public void ChangeInteractableStatus(bool status)
        {
            userName.interactable = status;
            password.interactable = status;
            loginButton.interactable = status;

            confirmPassword.interactable = status;
            registerButton.interactable = status;
            cancelRegisterButton.interactable = status;

            loginFacebookButton.interactable = status;
            loginGoogleButton.interactable = status;

            rememberMe.interactable = status;
            optionsButton.interactable = status;
        }

        #endregion
    }
}
