using UnityEngine;

namespace _Project.UI.Login.Scripts
{
    public class LoginCanvasManager : MonoBehaviour
    {
        public static LoginCanvasManager Instance;
        
        #region SerializeFields

        [SerializeField] private GameObject loginScreen;
        [SerializeField] private GameObject registerScreen;

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

        

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        #endregion

        #region Private Methods

        

        #endregion

        #region Public Methods

        public void ActivateLoginScreen()
        {
            loginScreen.SetActive(true);
            registerScreen.SetActive(false);
        }

        public void ActivateRegisterScreen(string userNameText)
        {
            if (!string.IsNullOrEmpty(userNameText))
            {
                registerScreen.GetComponent<RegisterScreen>().SetUserName(userNameText);
            }

            registerScreen.SetActive(true);
            loginScreen.SetActive(false);
        }

        #endregion
    }
}
