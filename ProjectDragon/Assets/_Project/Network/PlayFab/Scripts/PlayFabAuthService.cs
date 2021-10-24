﻿using System;
using Facebook.Unity;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using LoginResult = PlayFab.ClientModels.LoginResult;

namespace _Project.Network.PlayFab.Scripts
{
#if FACEBOOK
using Facebook.Unity;
#endif

    public enum AuthTypes
    {
        None,
        Silent,
        EmailAndPassword,
        RegisterPlayFabAccount,
        Facebook
    }

    public class PlayFabAuthService
    {
        public static PlayFabAuthService Instance => instance ??= new PlayFabAuthService();
        private static PlayFabAuthService instance;

        #region Constuctor

        private PlayFabAuthService()
        {
            instance = this;
        }

        #endregion

        #region Private Fields

        private const string LoginRememberKey = "PlayFabLoginRemember";
        private const string PlayFabRememberMeIdKey = "PlayFabIdPassGuid";
        private const string PlayFabAuthTypeKey = "PlayFabAuthType";

        #endregion

        #region Public Fields

        public string Email;
        public string UserName;
        public string Password;
        public string AuthTicket;
        public GetPlayerCombinedInfoRequestParams InfoRequestParams;

        //this is a force link flag for custom ids for demoing
        public readonly bool ForceLink = false;

        #endregion

        #region Public Properties

        //Accessibility for PlayFab ID & Session Tickets
        public static string PlayFabId { get; private set; }

        public static string SessionTicket { get; private set; }

        /// <summary>
        /// Remember the user next time they log in
        /// This is used for Auto-Login purpose.
        /// </summary>
        public static bool RememberMe
        {
            get => PlayerPrefs.GetInt(LoginRememberKey, 0) != 0;
            set => PlayerPrefs.SetInt(LoginRememberKey, value ? 1 : 0);
        }

        /// <summary>
        /// Remember the type of authenticate for the user
        /// </summary>
        public static AuthTypes AuthType
        {
            get => (AuthTypes) PlayerPrefs.GetInt(PlayFabAuthTypeKey, 0);
            internal set => PlayerPrefs.SetInt(PlayFabAuthTypeKey, (int) value);
        }

        /// <summary>
        /// Generated Remember Me ID
        /// Pass Null for a value to have one auto-generated.
        /// </summary>
        private static string RememberMeId
        {
            get => PlayerPrefs.GetString(PlayFabRememberMeIdKey, "");
            set
            {
                var guid = string.IsNullOrEmpty(value) ? Guid.NewGuid().ToString() : value;
                PlayerPrefs.SetString(PlayFabRememberMeIdKey, guid);
            }
        }

        #endregion

        #region Events

        public static event Action OnDisplayAuthentication;

        public static event Action<LoginResult> OnLoginSuccess;

        public static event Action<PlayFabError> OnPlayFabError;

        #endregion

        #region Private Methods

        /// <summary>
        /// Authenticate a user in PlayFab using an Email & Password combo
        /// </summary>
        private void AuthenticateEmailPassword()
        {
            Debug.Log(RememberMe);
            Debug.Log(RememberMeId);

            //Check if the users has opted to be remembered.
            if (RememberMe && !string.IsNullOrEmpty(RememberMeId))
            {
                //If the user is being remembered, then log them in with a customid that was 
                //generated by the RememberMeId property
                PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
                {
                    TitleId = PlayFabSettings.TitleId,
                    CustomId = RememberMeId,
                    CreateAccount = true,
                    InfoRequestParameters = InfoRequestParams
                }, (result) =>
                {
                    //Store identity and session
                    PlayFabId = result.PlayFabId;
                    SessionTicket = result.SessionTicket;

                    //report login result back to subscriber
                    OnLoginSuccess?.Invoke(result);
                }, (error) =>
                {
                    //report error back to subscriber
                    OnPlayFabError?.Invoke(error);
                });
                return;
            }

            //a good catch: If username & password is empty, then do not continue, and Call back to Authentication UI Display 
            if (!RememberMe && string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Password))
            {
                OnDisplayAuthentication?.Invoke();
                return;
            }

            //We have not opted for remember me in a previous session, so now we have to login the user with email & password.
            PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                Email = Email,
                Password = Password,
                InfoRequestParameters = InfoRequestParams
            }, (result) =>
            {
                //store identity and session
                PlayFabId = result.PlayFabId;
                SessionTicket = result.SessionTicket;

                //Note: At this point, they already have an account with PlayFab using a Username (email) & Password
                //If RememberMe is checked, then generate a new Guid for Login with CustomId.
                if (RememberMe)
                {
                    RememberMeId = Guid.NewGuid().ToString();
                    AuthType = AuthTypes.EmailAndPassword;
                    //Fire and forget, but link a custom ID to this PlayFab Account.
                    PlayFabClientAPI.LinkCustomID(new LinkCustomIDRequest()
                    {
                        CustomId = RememberMeId,
                        ForceLink = ForceLink
                    }, null, null);
                }

                //report login result back to subscriber
                OnLoginSuccess?.Invoke(result);
            }, (error) =>
            {
                //Report error back to subscriber
                OnPlayFabError?.Invoke(error);
            });
        }

        private void AuthenticateFacebook()
        {
            Debug.Log("Facebook Auth Complete! Access Token: " + AccessToken.CurrentAccessToken.TokenString +
                      "\nLogging into PlayFab...");

            /*
            * We proceed with making a call to PlayFab API. We pass in current Facebook AccessToken and let it create
            * and account using CreateAccount flag set to true. We also pass the callback for Success and Failure results
            */
            PlayFabClientAPI.LoginWithFacebook(
                new LoginWithFacebookRequest
                    {CreateAccount = true, AccessToken = AccessToken.CurrentAccessToken.TokenString},
                OnPlayFabFacebookAuthComplete, OnPlayFabFacebookAuthFailed);
        }

        private void AuthenticateGoogle()
        {
            PlayFabClientAPI.LoginWithGoogleAccount(new LoginWithGoogleAccountRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                InfoRequestParameters = InfoRequestParams
            }, (result) =>
            {
                //store identity and session
                PlayFabId = result.PlayFabId;
                SessionTicket = result.SessionTicket;

                //Note: At this point, they already have an account with PlayFab using a Username (email) & Password
                //If RememberMe is checked, then generate a new Guid for Login with CustomId.
                if (RememberMe)
                {
                    RememberMeId = Guid.NewGuid().ToString();
                    AuthType = AuthTypes.EmailAndPassword;
                    //Fire and forget, but link a custom ID to this PlayFab Account.
                    PlayFabClientAPI.LinkCustomID(new LinkCustomIDRequest()
                    {
                        CustomId = RememberMeId,
                        ForceLink = ForceLink
                    }, null, null);
                }

                //report login result back to subscriber
                OnLoginSuccess?.Invoke(result);
            }, (error) =>
            {
                //Report error back to subscriber
                OnPlayFabError?.Invoke(error);
            });
        }

        /// <summary>
        /// Register a user with an Email & Password
        /// </summary>
        private void AddAccountAndPassword()
        {
            //Any time we attempt to register a player, first silently authenticate the player.
            //This will retain the players True Origination (Android, iOS, Desktop)
            SilentlyAuthenticate((result) =>
            {
                if (result == null)
                {
                    //something went wrong with Silent Authentication, Check the debug console.
                    OnPlayFabError?.Invoke(new PlayFabError()
                    {
                        Error = PlayFabErrorCode.UnknownError,
                        ErrorMessage = "Silent Authentication by Device failed"
                    });
                }

                //Now add our username & password.
                if (result != null)
                    PlayFabClientAPI.AddUsernamePassword(new AddUsernamePasswordRequest()
                    {
                        Username = !string.IsNullOrEmpty(UserName)
                            ? UserName
                            : result.PlayFabId, //Because it is required & Unique and not supplied by User.
                        Email = Email,
                        Password = Password,
                    }, (addResult) =>
                    {
                        if (OnLoginSuccess == null)
                            return;
                        //Store identity and session
                        PlayFabId = result.PlayFabId;
                        SessionTicket = result.SessionTicket;

                        //If they opted to be remembered on next login.
                        if (RememberMe)
                        {
                            //Generate a new Guid 
                            RememberMeId = Guid.NewGuid().ToString();
                            //Fire and forget, but link the custom ID to this PlayFab Account.
                            PlayFabClientAPI.LinkCustomID(new LinkCustomIDRequest()
                            {
                                CustomId = RememberMeId,
                                ForceLink = ForceLink
                            }, null, null);
                        }

                        //Override the auth type to ensure next login is using this auth type.
                        AuthType = AuthTypes.EmailAndPassword;

                        //Report login result back to subscriber.
                        OnLoginSuccess?.Invoke(result);
                    }, (error) =>
                    {
                        //Report error result back to subscriber
                        OnPlayFabError?.Invoke(error);
                    });
            });
        }

        private void SilentlyAuthenticate(Action<LoginResult> callback = null)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        //Get the device id from native android
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
        AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
        string deviceId = secure.CallStatic<string>("getString", contentResolver, "android_id");

        //Login with the android device ID
        PlayFabClientAPI.LoginWithAndroidDeviceID(new LoginWithAndroidDeviceIDRequest() {
            TitleId = PlayFabSettings.TitleId,
            AndroidDevice = SystemInfo.deviceModel,
            OS = SystemInfo.operatingSystem,
            AndroidDeviceId = deviceId,
            CreateAccount = true,
            InfoRequestParameters = InfoRequestParams
        }, (result) => {
            
            //Store Identity and session
            PlayFabId = result.PlayFabId;
            SessionTicket = result.SessionTicket;

            //check if we want to get this callback directly or send to event subscribers.
            if (callback == null && OnLoginSuccess != null)
            {
                //report login result back to the subscriber
                OnLoginSuccess.Invoke(result);
            }else
            {
                //report login result back to the caller
                callback?.Invoke(result);
            }
        }, (error) => {

            //report error back to the subscriber
            if(callback == null && OnPlayFabError != null){
                OnPlayFabError?.Invoke(error);
            }else
            {
                //make sure the loop completes, callback with null
                callback?.Invoke(null);
                //Output what went wrong to the console.
                Debug.LogError(error.GenerateErrorReport());
            }
        });

#elif UNITY_IPHONE || UNITY_IOS && !UNITY_EDITOR
        PlayFabClientAPI.LoginWithIOSDeviceID(new LoginWithIOSDeviceIDRequest() {
            TitleId = PlayFabSettings.TitleId,
            DeviceModel = SystemInfo.deviceModel, 
            OS = SystemInfo.operatingSystem,
            DeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = InfoRequestParams
        }, (result) => {
            //Store Identity and session
            _playFabId = result.PlayFabId;
            _sessionTicket = result.SessionTicket;

            //check if we want to get this callback directly or send to event subscribers.
            if (callback == null && OnLoginSuccess != null)
            {
                //report login result back to the subscriber
                OnLoginSuccess.Invoke(result);
            }else
            {
                //report login result back to the caller
                callback?.Invoke(result);
            }
        }, (error) => {
            //report error back to the subscriber
            if(callback == null && OnPlayFabError != null){
                OnPlayFabError?.Invoke(error);
            }else{
                //make sure the loop completes, callback with null
                callback?.Invoke(null);
                //Output what went wrong to the console.
                Debug.LogError(error.GenerateErrorReport());
            }
        });
#else
            PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true,
                InfoRequestParameters = InfoRequestParams
            }, (result) =>
            {
                //Store Identity and session
                PlayFabId = result.PlayFabId;
                SessionTicket = result.SessionTicket;

                //check if we want to get this callback directly or send to event subscribers.
                if (callback == null && OnLoginSuccess != null)
                {
                    //report login result back to the subscriber
                    OnLoginSuccess.Invoke(result);
                }
                else
                {
                    //report login result back to the caller
                    callback?.Invoke(result);
                }
            }, (error) =>
            {
                //report error back to the subscriber
                if (callback == null && OnPlayFabError != null)
                {
                    OnPlayFabError?.Invoke(error);
                }
                else
                {
                    //make sure the loop completes, callback with null
                    callback?.Invoke(null);
                    //Output what went wrong to the console.
                    Debug.LogError(error.GenerateErrorReport());
                }
            });
#endif
        }

        // When processing both results, we just set the message, explaining what's going on.
        private static void OnPlayFabFacebookAuthComplete(LoginResult loginResult)
        {
            Debug.Log("PlayFab Facebook Auth Complete. Session ticket: " + loginResult.SessionTicket);
        }

        private static void OnPlayFabFacebookAuthFailed(PlayFabError error)
        {
            Debug.LogFormat("PlayFab Facebook Auth Failed: " + error.GenerateErrorReport(), true);
        }

        #endregion

        #region Public Methods

        internal static void ClearRememberMe()
        {
            PlayerPrefs.DeleteKey(LoginRememberKey);
            PlayerPrefs.DeleteKey(PlayFabRememberMeIdKey);
        }

        /// <summary>
        /// Kick off the authentication process by specific authType.
        /// </summary>
        /// <param name="authType"></param>
        internal void Authenticate(AuthTypes authType)
        {
            AuthType = authType;
            Authenticate();
        }

        /// <summary>
        /// Authenticate the user by the Auth Type that was defined.
        /// </summary>
        internal void Authenticate()
        {
            switch (AuthType)
            {
                case AuthTypes.None:
                    OnDisplayAuthentication?.Invoke();
                    break;
                case AuthTypes.Silent:
                    SilentlyAuthenticate();
                    break;
                case AuthTypes.EmailAndPassword:
                    AuthenticateEmailPassword();
                    break;
                case AuthTypes.RegisterPlayFabAccount:
                    AddAccountAndPassword();
                    break;
                case AuthTypes.Facebook:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal void UnlinkSilentAuth()
        {
            SilentlyAuthenticate((result) =>
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            //Get the device id from native android
            AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
            AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
            string deviceId = secure.CallStatic<string>("getString", contentResolver, "android_id");

            //Fire and forget, unlink this android device.
            PlayFabClientAPI.UnlinkAndroidDeviceID(new UnlinkAndroidDeviceIDRequest() {
                AndroidDeviceId = deviceId
            }, null, null);

#elif UNITY_IPHONE || UNITY_IOS && !UNITY_EDITOR
            PlayFabClientAPI.UnlinkIOSDeviceID(new UnlinkIOSDeviceIDRequest()
            {
                DeviceId = SystemInfo.deviceUniqueIdentifier
            }, null, null);
#else
                PlayFabClientAPI.UnlinkCustomID(new UnlinkCustomIDRequest()
                {
                    CustomId = SystemInfo.deviceUniqueIdentifier
                }, null, null);
#endif
            });
        }

        #endregion
    }
}