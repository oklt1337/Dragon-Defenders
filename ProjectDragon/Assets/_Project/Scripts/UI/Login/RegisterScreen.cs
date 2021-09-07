using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Login
{
    public class RegisterScreen : MonoBehaviour, ICanvas
    {
        [SerializeField] private TMP_InputField email;
        [SerializeField] private TMP_InputField userName;
        [SerializeField] private TMP_InputField password;
        [SerializeField] private TMP_InputField repeatPassword;
        [SerializeField] private Button registerButton;
        [SerializeField] private Button backButton;

        public static Action<string, string, string> OnTryRegister;

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

        #region Pubic Methods

        public void OnClickRegister()
        {
            if (string.IsNullOrEmpty(email.text) || string.IsNullOrEmpty(userName.text) ||
                string.IsNullOrEmpty(password.text) || string.IsNullOrEmpty(repeatPassword.text))
                return;

            if (password.text == repeatPassword.text)
            {
                OnTryRegister?.Invoke(email.text, userName.text, password.text);

                email.text = string.Empty;
                userName.text = string.Empty;
                password.text = string.Empty;
                repeatPassword.text = string.Empty;
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
        }

        public void OnSelectedPassword()
        {
            password.textComponent.color = Color.black;
            repeatPassword.textComponent.color = Color.black;
        }

        #endregion

        #region ICanvas

        public void ChangeInteractableStatus(bool status)
        {
            email.interactable = status;
            userName.interactable = status;
            password.interactable = status;
            repeatPassword.interactable = status;
            registerButton.interactable = status;
            backButton.interactable = status;
        }

        #endregion
    }
}
