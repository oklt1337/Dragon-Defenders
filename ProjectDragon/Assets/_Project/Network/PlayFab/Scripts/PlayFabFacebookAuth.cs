using Facebook.Unity;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using LoginResult = Facebook.Unity.LoginResult;

namespace _Project.Network.PlayFab.Scripts
{
    public class PlayFabFacebookAuth : MonoBehaviour
    {
        // holds the latest message to be displayed on the screen
        private string message;

        public void Start()
        {
            SetMessage("Initializing Facebook..."); // logs the given message and displays it on the screen using OnGUI method

            // This call is required before any other calls to the Facebook API. We pass in the callback to be invoked once initialization is finished
            FB.Init(OnFacebookInitialized);
        }

        private void OnFacebookInitialized()
        {
            SetMessage("Logging into Facebook...");

            // Once Facebook SDK is initialized, if we are logged in, we log out to demonstrate the entire authentication cycle.
            if (FB.IsLoggedIn)
                FB.LogOut();

            // We invoke basic login procedure and pass in the callback to process the result
            FB.LogInWithReadPermissions(null, OnFacebookLoggedIn);
        }

        private void OnFacebookLoggedIn(ILoginResult result)
        {
            // If result has no errors, it means we have authenticated in Facebook successfully
            if (result == null || string.IsNullOrEmpty(result.Error))
            {
                SetMessage("Facebook Auth Complete! Access Token: " + AccessToken.CurrentAccessToken.TokenString + "\nLogging into PlayFab...");

                /*
             * We proceed with making a call to PlayFab API. We pass in current Facebook AccessToken and let it create
             * and account using CreateAccount flag set to true. We also pass the callback for Success and Failure results
             */
                PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest { CreateAccount = true, AccessToken = AccessToken.CurrentAccessToken.TokenString},
                    OnPlayFabFacebookAuthComplete, OnPlayFabFacebookAuthFailed);
            }
            else
            {
                // If Facebook authentication failed, we stop the cycle with the message
                SetMessage("Facebook Auth Failed: " + result.Error + "\n" + result.RawResult, true);
            }
        }

        // When processing both results, we just set the message, explaining what's going on.
        private void OnPlayFabFacebookAuthComplete(global::PlayFab.ClientModels.LoginResult loginResult)
        {
            SetMessage("PlayFab Facebook Auth Complete. Session ticket: " + loginResult.SessionTicket);
        }

        private void OnPlayFabFacebookAuthFailed(PlayFabError error)
        {
            SetMessage("PlayFab Facebook Auth Failed: " + error.GenerateErrorReport(), true);
        }

        private void SetMessage(string shownMessage, bool error = false)
        {
            message = shownMessage;
            if (error)
                Debug.LogError(message);
            else
                Debug.Log(message);
        }

        public void OnGUI()
        {
            /*var style = new GUIStyle { fontSize = 40, normal = new GUIStyleState { textColor = Color.white }, alignment = TextAnchor.MiddleCenter, wordWrap = true };
            var area = new Rect(0,0,Screen.width,Screen.height);
            GUI.Label(area, message,style);*/
        }
    }
}
