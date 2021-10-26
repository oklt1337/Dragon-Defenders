using System;
using Facebook.Unity;
using UnityEngine;

namespace _Project.Network.PlayFab.Scripts
{
    public class PlayFabFacebookAuth
    {
        public static PlayFabFacebookAuth Instance => instance ??= new PlayFabFacebookAuth();
        private static PlayFabFacebookAuth instance;

        #region Constructor

        private PlayFabFacebookAuth()
        {
            
        }

        #endregion

        #region Public Properties

        public static ILoginResult LoginResult { get; private set; }

        #endregion
        
        #region Events

        public static event Action<string, string, AuthTypes> OnFacebookInitializedDone;

        #endregion

        #region Private Methods

        private static void OnFacebookInitialized()
        {
            Debug.Log("Logging into Facebook...");

            // Once Facebook SDK is initialized, if we are logged in, we log out to demonstrate the entire authentication cycle.
            if (FB.IsLoggedIn)
                FB.LogOut();

            // We invoke basic login procedure and pass in the callback to process the result
            FB.LogInWithReadPermissions(null, delegate(ILoginResult result)
            {
                LoginResult = result;
                OnFacebookInitializedDone?.Invoke(null, null, AuthTypes.Facebook);
            });
        }

        #endregion

        #region Public Methods

        public void ConnectWithFacebook()
        {
            Debug.Log("Initializing Facebook..."); // logs the given message and displays it on the screen using OnGUI method

            // This call is required before any other calls to the Facebook API. We pass in the callback to be invoked once initialization is finished
            FB.Init(OnFacebookInitialized);
        }

        #endregion
    }
}
