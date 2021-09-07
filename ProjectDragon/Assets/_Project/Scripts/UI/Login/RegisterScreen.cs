using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Login
{
    public class RegisterScreen : MonoBehaviour, ICanvas
    {
        [SerializeField] private TMP_InputField email;
        [SerializeField] private TMP_InputField userName;
        [SerializeField] private TMP_InputField password;
        [SerializeField] private TMP_InputField repeatPassword;
        
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

        #region ICanvas

        public void ChangeInteractableStatus(bool status)
        {
            email.interactable = status;
            userName.interactable = status;
            password.interactable = status;
            repeatPassword.interactable = status;
        }

        #endregion
    }
}
