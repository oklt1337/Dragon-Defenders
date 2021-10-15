using _Project.Network.PlayFab.Scripts;
using _Project.UI.Managers.Scripts;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayFabErrorCode = PlayFab.PlayFabErrorCode;

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

        #endregion

        #region Unity Methods

        private void Awake()
        {
            //Clear PlayerPrefs and Unlink Silent Auth.
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
            loginButton.onClick.AddListener(OnClickLogin);
            loginFacebookButton.onClick.AddListener(OnClickLoginWithFacebook);
            loginGoogleButton.onClick.AddListener(OnClickLoginWithGoogle);
            registerButton.onClick.AddListener(OnClickRegister);
            cancelRegisterButton.onClick.AddListener(OnClickCancelRegister);

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
        /// Login Successfully
        /// </summary>
        /// <param name="result"></param>
        private void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
        {
            Debug.LogFormat("Logged In as: {0}", result.PlayFabId);

            //Deactivate our screen if we logged in successfully.
            panel.SetActive(false);
        }

        /// <summary>
        /// Error handling for when Login returns errors.
        /// </summary>
        /// <param name="error">PlayFabError</param>
        private void OnPlayFabError(PlayFab.PlayFabError error)
        {
            // if we didnt find the account we try to register him.
            if (error.Error is PlayFabErrorCode.AccountNotFound)
            {
                registerPanel.SetActive(true);
                return;
            }
            
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
        /// Login with email / password combo
        /// Note: in this flow if no account is found, it will ask them to register.
        /// </summary>
        private void OnClickLogin()
        {
            Debug.Log("Click Login");
            _authService.Email = userName.text;
            _authService.Password = password.text;
            _authService.Authenticate(AuthTypes.EmailAndPassword);
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

            _authService.Email = userName.text;
            _authService.Password = password.text;
            _authService.Authenticate(AuthTypes.RegisterPlayFabAccount);
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
