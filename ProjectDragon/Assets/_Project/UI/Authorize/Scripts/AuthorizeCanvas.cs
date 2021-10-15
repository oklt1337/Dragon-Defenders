using System;
using _Project.Network.PlayFab.Scripts;
using _Project.UI.Managers.Scripts;
using PlayFab.ClientModels;
using PlayFab.PfEditor.EditorModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayFabErrorCode = PlayFab.PlayFabErrorCode;

namespace _Project.UI.Authorize.Scripts
{
    public class AuthorizeCanvas : MonoBehaviour, ICanvas
    {
        #region SerialzeFields

        [SerializeField] private TMP_InputField userName;
        [SerializeField] private TMP_InputField password;
        [SerializeField] private TMP_InputField confirmPassword;
        [SerializeField] private Button loginButton;
        [SerializeField] private Button registerButton;
        [SerializeField] private Button cancelRegisterButton;
        [SerializeField] private Button loginFacebookButton;
        [SerializeField] private Button loginGoogleButton;
        [SerializeField] private Toggle rememberMe;
        
        [SerializeField] private GameObject registerPanel;
        [SerializeField] private GameObject panel;

        [SerializeField] private GetPlayerCombinedInfoRequestParams infoRequestParams;

        #endregion

        #region Private Fields
        
        private bool _clearPlayerPrefs;
        private readonly PlayFabAuthService _authService = PlayFabAuthService.Instance;

        #endregion

        #region Protected Fields

        

        #endregion

        #region Public Fields

        

        #endregion

        #region Public Properties

        

        #endregion

        #region Events

        public static Action<string, string, bool> OnTryLogin;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (_clearPlayerPrefs)
            {
                _authService.UnlinkSilentAuth();
                _authService.ClearRememberMe();
                _authService.AuthType = AuthTypes.None;
            }

            //Set our remember me button to our remembered state.
            rememberMe.isOn = _authService.RememberMe;
        }
        
        public void Start()
        {

            //Hide all our panels until we know what UI to display
            panel.SetActive(false);
            registerPanel.SetActive(false);

            //Subscribe to events that happen after we authenticate
            PlayFabAuthService.OnDisplayAuthentication += OnDisplayAuthentication;
            PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;
            PlayFabAuthService.OnPlayFabError += OnPlayFabError;

            //Bind auth remember me status to toggle.
            rememberMe.onValueChanged.AddListener((arg0 => _authService.RememberMe = arg0));

            //Bind to UI buttons to perform actions when user interacts with the UI.
            loginButton.onClick.AddListener(OnLoginClicked);
            loginFacebookButton.onClick.AddListener(OnLoginWithFacebookClicked);
            loginGoogleButton.onClick.AddListener(OnLoginWithGoogleClicked);
            registerButton.onClick.AddListener(OnRegisterButtonClicked);
            cancelRegisterButton.onClick.AddListener(OnCancelRegisterButtonClicked);

            //Set the data we want at login from what we chose in our meta data.
            _authService.InfoRequestParams = infoRequestParams;

            //Start the authentication process.
            _authService.Authenticate();
        }

        private void OnEnable()
        {
            //CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
        }

        #endregion
        
        #region Private Methods
        
        /// <summary>
    /// Login Successfully - Goes to next screen.
    /// </summary>
    /// <param name="result"></param>
    private void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
    {
        Debug.LogFormat("Logged In as: {0}", result.PlayFabId);
        
        //Show our next screen if we logged in successfully.
        panel.SetActive(false);
    }

    /// <summary>
    /// Error handling for when Login returns errors.
    /// </summary>
    /// <param name="error"></param>
    private void OnPlayFabError(PlayFab.PlayFabError error)
    {
        //There are more cases which can be caught, below are some
        //of the basic ones.
        if (error.Error is PlayFabErrorCode.AccountNotFound)
        {
            registerPanel.SetActive(true);
            return;
        }

        //Also report to debug console, this is optional.
        Debug.Log(error.Error);
        Debug.LogError(error.GenerateErrorReport());
    }

    /// <summary>
    /// Choose to display the Auth UI or any other action.
    /// </summary>
    private void OnDisplayAuthentication()
    {
        //Here we have chooses what to do when AuthType is None.
        panel.SetActive(true);
    }

    /// <summary>
    /// Login Button means they've selected to submit a username (email) / password combo
    /// Note: in this flow if no account is found, it will ask them to register.
    /// </summary>
    private void OnLoginClicked()
    {
        Debug.Log("Click Login");
        _authService.Email = userName.text;
        _authService.Password = password.text;
        _authService.Authenticate(AuthTypes.EmailAndPassword);
    }

    /// <summary>
    /// No account was found, and they have selected to register a username (email) / password combo.
    /// </summary>
    private void OnRegisterButtonClicked()
    {
        if (password.text != confirmPassword.text)
        {
            Debug.Log("Passwords do not Match.");
            return;
        }

        _authService.Email = userName.text;
        _authService.Password = password.text;
        _authService.Authenticate(AuthTypes.RegisterPlayFabAccount);
    }

    /// <summary>
    /// They have opted to cancel the Registration process.
    /// Possibly they typed the email address incorrectly.
    /// </summary>
    private void OnCancelRegisterButtonClicked()
    {
        //Reset all forms
        userName.text = string.Empty;
        password.text = string.Empty;
        confirmPassword.text = string.Empty;
        //Show panels
        registerPanel.SetActive(false);
    }


    /// <summary>
    /// Login with a facebook account.  This kicks off the request to facebook
    /// </summary>
    private void OnLoginWithFacebookClicked()
    {
        Debug.Log("Logging In to Facebook..");
    }


    /// <summary>
    /// Login with a google account.  This kicks off the request to google play games.
    /// </summary>
    private void OnLoginWithGoogleClicked()
    {
        Debug.Log("Logging In to Google..");
    }

    #endregion

        #region Public Methods

        public void ChangeInteractableStatus(bool status)
        {
            userName.interactable = status;
            password.interactable = status;
            rememberMe.interactable = status;
            loginButton.interactable = status;
        }
        
        public void OnClickLogin()
        {
            if (string.IsNullOrEmpty(userName.text) || string.IsNullOrEmpty(password.text))
                return;
            
            OnTryLogin?.Invoke(userName.text, password.text, rememberMe.isOn);
        }

        #endregion
    }
}
