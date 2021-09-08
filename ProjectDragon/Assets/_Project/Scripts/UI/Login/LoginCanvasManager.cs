using UnityEngine;

namespace _Project.Scripts.UI.Login
{
    public class LoginCanvasManager : MonoBehaviour
    {
        public static LoginCanvasManager Instance;
        
        [SerializeField] private GameObject loginScreen;
        [SerializeField] private GameObject registerScreen;

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

        public void ActivateLoginScreen()
        {
            loginScreen.SetActive(true);
            registerScreen.SetActive(false);
        }

        public void ActivateRegisterScreen()
        {
            registerScreen.SetActive(true);
            loginScreen.SetActive(false);
        }
    }
}
