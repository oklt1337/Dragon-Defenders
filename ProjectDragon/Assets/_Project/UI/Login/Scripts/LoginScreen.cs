using System;
using _Project.UI.Managers.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.Login.Scripts
{
    public class LoginScreen : MonoBehaviour, ICanvas
    {
        #region SerialzeFields

        [SerializeField] private TMP_InputField userName;
        [SerializeField] private TMP_InputField password;
        [SerializeField] private Toggle rememberMe;
        [SerializeField] private Button loginButton;
        [SerializeField] private Button registerButton;

        #endregion

        #region Private Fields

        

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

        private void OnEnable()
        {
            CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
        }

        #endregion
        
        #region Private Methods

        private void ClearInputs()
        {
            userName.text = string.Empty;
            password.text = string.Empty;
        }

        #endregion

        #region Public Methods

        public void ChangeInteractableStatus(bool status)
        {
            userName.interactable = status;
            password.interactable = status;
            rememberMe.interactable = status;
            loginButton.interactable = status;
            registerButton.interactable = status;
        }
        
        public void OnClickLogin()
        {
            if (string.IsNullOrEmpty(userName.text) || string.IsNullOrEmpty(password.text))
                return;
            
            OnTryLogin?.Invoke(userName.text, password.text, rememberMe.isOn);
            ClearInputs();
        }

        public void OnClickRegister()
        {
            LoginCanvasManager.Instance.ActivateRegisterScreen(userName.text);
            ClearInputs();
        }
        
        #endregion
    }
}
