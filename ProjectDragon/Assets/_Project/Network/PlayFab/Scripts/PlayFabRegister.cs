using System;
using _Project.UI.Login.Scripts;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace _Project.Network.PlayFab.Scripts
{
    public class PlayFabRegister : MonoBehaviour
    {
        #region Serializable Fields

        #endregion
        
        #region Private Fields
        
        private string _userName;
        private string _email;
        private string _password;

        #endregion

        #region Public Fields

        #endregion

        #region Pubic Events

        public static event Action<string,string,bool> OnRegisterSuccess;
        public static event Action<string> OnRegisterFailed;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            RegisterScreen.OnTryRegister += Register;
        }

        private void OnDestroy()
        {
            RegisterScreen.OnTryRegister -= Register;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check if Username is Valid
        /// for a Username to be valid it must be
        /// more then 3 and less than 24 characters.
        /// </summary>
        /// <returns>if userName is valid or not (bool)</returns>
        private bool IsValidUserName()
        {
            return _userName.Length >= 3 && _userName.Length <= 24;
        }

        /// <summary>
        /// Check if Password is Valid
        /// for a Password to be valid it must have
        /// more then 6 character.
        /// </summary>
        /// <returns>if password is valid or not (bool)</returns>
        private bool IsValidPassword()
        {
            return _password.Length >= 6;
        }

        /// <summary>
        /// Sending Request to PlayFab with userName and Password.
        /// If Request Fails get Callback with OnFailedToLogin
        /// If Request is Successful get Callback with OnLoginPlayFabSuccess
        /// </summary>
        private void RegisterPlayFabUserRequest()
        {
            Debug.Log($"Try register to PlayFab as {_userName}");

            RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest
            {
                Username = _userName,
                Email = _email,
                Password = _password
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterPlayFabSuccess, OnFailedToRegister);
        }
        
        /// <summary>
        /// Login in to PlayFab.
        /// </summary>
        /// <param name="email">email register with</param>
        /// <param name="userName">userName registers with</param>
        /// <param name="password">password registers with</param>
        private void Register(string email, string userName, string password)
        {
            _email = email;
            _userName = userName;
            _password = password;
            
            if (!IsValidUserName() || !IsValidPassword()) return;
            RegisterPlayFabUserRequest();
        }

        #endregion

        #region Public Methods

        #endregion

        #region PlayFab Callbacks

        private void OnRegisterPlayFabSuccess(RegisterPlayFabUserResult result)
        {
            Debug.Log($"Registration Successful: {result.PlayFabId}");

            OnRegisterSuccess?.Invoke(_userName, _password, false);
        }
        
        private static void OnFailedToRegister(PlayFabError error)
        {
            Debug.LogError($"ERROR {error.GenerateErrorReport()}");

            OnRegisterFailed?.Invoke(error.GenerateErrorReport());
        }

        #endregion
    }
}
