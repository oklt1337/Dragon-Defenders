using System;
using _Project.UI.Managers.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.Login.Scripts
{
    public class RegisterScreen : MonoBehaviour, ICanvas
    {
        #region SerializeFields

        [SerializeField] private TMP_InputField email;
        [SerializeField] private TMP_InputField userName;
        [SerializeField] private TMP_InputField password;
        [SerializeField] private TMP_InputField repeatPassword;
        [SerializeField] private Button registerButton;
        [SerializeField] private Button backButton;

        #endregion

        #region Priavte Fields

        

        #endregion

        #region Protected Fields

        

        #endregion

        #region Public Fields

        

        #endregion

        #region Public Properties

        

        #endregion

        #region Events

        public static Action<string, string, string> OnTryRegister;

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
            email.text = string.Empty;
            userName.text = string.Empty;
            password.text = string.Empty;
            repeatPassword.text = string.Empty;
        }

        #endregion

        #region Pubic Methods
        
        public void ChangeInteractableStatus(bool status)
        {
            email.interactable = status;
            userName.interactable = status;
            password.interactable = status;
            repeatPassword.interactable = status;
            registerButton.interactable = status;
            backButton.interactable = status;
        }

        public void OnClickRegister()
        {
            if (string.IsNullOrEmpty(email.text) || string.IsNullOrEmpty(userName.text) ||
                string.IsNullOrEmpty(password.text) || string.IsNullOrEmpty(repeatPassword.text))
                return;

            if (password.text == repeatPassword.text)
            {
                OnTryRegister?.Invoke(email.text, userName.text, password.text);

                ClearInputs();
            }
            else
            {
                password.textComponent.color = Color.red;
                repeatPassword.textComponent.color = Color.red;
            }
        }

        public void OnClickBack()
        {
            LoginCanvasManager.Instance.ActivateLoginScreen();
            ClearInputs();
        }

        public void OnSelectedPassword()
        {
            password.textComponent.color = Color.black;
            repeatPassword.textComponent.color = Color.black;
        }

        public void SetUserName(string userNameText)
        {
            userName.text = userNameText;
        }

        #endregion
    }
}
