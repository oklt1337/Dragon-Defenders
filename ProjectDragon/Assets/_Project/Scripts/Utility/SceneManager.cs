using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Utility
{
    public enum Scene
    {
        Intro,
        LogReg,
        MainMenu,
        Lobby,
        InGame
    }
    
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;
        
        public UnityEngine.SceneManagement.Scene CurrentScene => UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        /// <summary>
        /// Change Scene with Photon LoadLevel
        /// </summary>
        /// <param name="newScene">enum Scene</param>
        public void ChangeScene(Scene newScene)
        {
            PhotonNetwork.LoadLevel(newScene.ToString());
        }

        /// <summary>
        /// Change Scene with Unity SceneManager Async
        /// </summary>
        /// <param name="newScene">enum Scene</param>
        public void ChangeSceneAsync(Scene newScene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(newScene.ToString());
        }
    }
}
