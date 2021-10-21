using _Project.UI.Managers.Scripts;
using _Project.Utility.SceneManager.Scripts;
using Photon.Pun;

namespace _Project.UI.MainMenu.Settings_Screen.Scripts
{
    public class MainMenuSettingsScreen : MonoBehaviourPun, ICanvas
    {
        public void OnClickLogOut()
        {
           // PlayFabManager.
           // PlayFabAuthManager.LogOut();
            SceneManager.ChangeScene(Scene.Authorize);
        }
        
        public void ChangeInteractableStatus(bool status)
        {
            throw new System.NotImplementedException();
        }
    }
}
