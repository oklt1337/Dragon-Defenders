using System;
using _Project.UI.Authorize.Scripts;
using _Project.UI.MainMenu.Settings_Screen.Scripts;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Scene = _Project.Utility.SceneManager.Scripts.Scene;
using SceneManager = _Project.Utility.SceneManager.Scripts.SceneManager;

namespace _Project.Network.PlayFab.Scripts
{
    public class PlayFabAuthManager : MonoBehaviour
    {
        
        #region Private Serializable Fields

        [SerializeField] private PlayFabFacebookAuth playFabFacebookAuth;
        [SerializeField] private GetPlayerCombinedInfoRequestParams infoRequestParams;

        #endregion

        #region Public Properties
        
        public PlayFabFacebookAuth PlayFabFacebookAuth => playFabFacebookAuth;
        public GetPlayerCombinedInfoRequestParams InfoRequestParams => infoRequestParams;
        public PlayFabAuthService AuthService { get; } = PlayFabAuthService.Instance;

        #endregion

        #region Events

        public event Action OnAccountNotFound;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            PlayFabAuthService.OnPlayFabError += OnPlayFabError;
            PlayFabAuthService.OnLoginSuccess += delegate { SceneManager.ChangeScene(Scene.MainMenu); };
            PlayFabAuthService.OnDisplayAuthentication += DisplayAuthScene;
            AuthorizeCanvas.OnLogin += Authenticate;
            AuthorizeCanvas.OnRegister += Authenticate;
            PlayFabFacebookAuth.OnFacebookInitializedDone += Authenticate;
            MainMenuSettingsScreen.OnLogout += Logout;
            NetworkManager.Scripts.NetworkManager.Instance.OnAllServicesConnected += StartAuthenticationsProcess;
        }

        private void OnDisable()
        {
            PlayFabAuthService.OnPlayFabError -= OnPlayFabError;
            PlayFabAuthService.OnDisplayAuthentication -= DisplayAuthScene;
            AuthorizeCanvas.OnLogin -= Authenticate;
            AuthorizeCanvas.OnRegister -= Authenticate;
            PlayFabFacebookAuth.OnFacebookInitializedDone -= Authenticate;
            MainMenuSettingsScreen.OnLogout -= Logout;
        }

        #endregion

        #region Private Methods

        private void StartAuthenticationsProcess()
        {
            //Set the data we want at login from what we chose in our meta data.
            AuthService.InfoRequestParams = InfoRequestParams;
            
            //Start the authentication process.
            AuthService.Authenticate();
        }

        private void Authenticate(string email, string password, AuthTypes authTypes)
        {
            switch (authTypes)
            {
                case AuthTypes.None:
                    break;
                case AuthTypes.Silent:
                    break;
                case AuthTypes.EmailAndPassword:
                    AuthService.Email = email;
                    AuthService.Password = password;
                    AuthService.Authenticate(authTypes);
                    break;
                case AuthTypes.RegisterPlayFabAccount:
                    AuthService.Email = email;
                    AuthService.Password = password;
                    AuthService.Authenticate(authTypes);
                    break;
                case AuthTypes.Facebook:
                    // If result has no errors, it means we have authenticated in Facebook successfully
                    if (PlayFabFacebookAuth.LoginResult == null || string.IsNullOrEmpty(PlayFabFacebookAuth.LoginResult.Error))
                    {
                        AuthService.Authenticate(authTypes);
                    }
                    else
                    {
                        // If Facebook authentication failed, we stop the cycle with the message
                        Debug.LogFormat("Facebook Auth Failed: " + PlayFabFacebookAuth.LoginResult.Error + "\n" + PlayFabFacebookAuth.LoginResult.RawResult, true);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(authTypes), authTypes, null);
            }
        }

        /// <summary>
        /// ForgetsAllCredentials, UnlinksSilentAuth and ClearsRememberMe
        /// </summary>
        private void Logout()
        {
            PlayFabClientAPI.ForgetAllCredentials();
            AuthService.UnlinkSilentAuth();
            PlayFabAuthService.ClearRememberMe();
            PlayFabAuthService.AuthType = AuthTypes.None;
            
            DisplayAuthScene();
        }

        /// <summary>
        /// Load Authenticate Scene
        /// </summary>
        private static void DisplayAuthScene()
        {
            SceneManager.ChangeScene(Scene.Authorize);
        }

        #endregion

        #region PlayFab Callbacks

        /// <summary>
        /// Error handling for when Login returns errors.
        /// </summary>
        /// <param name="error">PlayFabError</param>
        private void OnPlayFabError(PlayFabError error)
        {
            // if we didnt find the account we try to register him.
            if (error.Error == PlayFabErrorCode.AccountNotFound)
            {
                OnAccountNotFound?.Invoke();
                return;
            }
            
            Debug.Log(error.Error);
            Debug.LogError(error.GenerateErrorReport());
            
            //Clear all save data
            PlayFabClientAPI.ForgetAllCredentials();
            AuthService.UnlinkSilentAuth();
            PlayFabAuthService.ClearRememberMe();
            PlayFabAuthService.AuthType = AuthTypes.None;
        }

        #endregion
    }
}
