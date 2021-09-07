using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Login
{
    public class LoginScreen : MonoBehaviour, ICanvas
    {
        [SerializeField] private TMP_InputField userName;
        [SerializeField] private TMP_InputField password;
        [SerializeField] private Toggle rememberMe;
        [SerializeField] private Button loginButton;
        [SerializeField] private Button registerButton;

        public static Action<string, string, bool?> OnTryLogin;

        #region Unity Methods

        private void OnEnable()
        {
            CanvasManager.Instance.Subscribe(this);
            userName.Select();
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
        }

        #endregion

        #region Public Methods

        public void OnClickLogin()
        {
            if (string.IsNullOrEmpty(userName.text) || string.IsNullOrEmpty(password.text))
                return;
            
            OnTryLogin?.Invoke(userName.text, password.text, rememberMe);
            userName.text = string.Empty;
            password.text = string.Empty;
        }

        public void OnClickRegister()
        {
            LoginCanvasManager.Instance.ActivateRegisterScreen();
        }
        
        #endregion

        #region ICanvas

        public void ChangeInteractableStatus(bool status)
        {
            userName.interactable = status;
            password.interactable = status;
            rememberMe.interactable = status;
            loginButton.interactable = status;
            registerButton.interactable = status;
        }

        #endregion
    }
}
