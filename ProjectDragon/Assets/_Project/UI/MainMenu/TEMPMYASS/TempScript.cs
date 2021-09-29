using _Project.Utility.SceneManager.Scripts;
using UnityEngine;

namespace _Project.UI.MainMenu.TEMPMYASS
{
    public class TempScript : MonoBehaviour
    {
        public void OnClickPlay()
        {
            SceneManager.ChangeScene(Scene.GameScene);
        }
    }
}
