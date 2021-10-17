using System;
using System.Collections;
using _Project.Network.NetworkManager.Scripts;
using Photon.Pun;
using UnityEngine;

namespace _Project.Utility.SceneManager.Scripts
{
    public enum Scene
    {
        Intro,
        Login,
        MainMenu,
        Lobby,
        GameScene
    }
    
    public class SceneManager : MonoBehaviour
    {
        #region SerialzeFields

        [SerializeField] private float loadingLoginSceneDelay = 3f;

        #endregion

        #region Private Fields

        private Coroutine _loadingLoginSceneCo;

        #endregion

        #region Public Properties

        public static Scene CurrentScene => GetCurrentScene();

        #endregion

        #region Unity Methods

        private void Start()
        {
            if (NetworkManager.Instance.PlayFabManager.PlayFabLogin.AutoLogin)
                return;
            
            LoadLoginScene();
        }

        #endregion

        #region Private Methods

        private static Scene GetCurrentScene()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex switch
            {
                0 => Scene.Intro,
                1 => Scene.Login,
                2 => Scene.MainMenu,
                3 => Scene.Lobby,
                4 => Scene.GameScene,
                _ => throw new IndexOutOfRangeException()
            };
        }

        private void LoadLoginScene()
        {
            if (_loadingLoginSceneCo != null)
                StopCoroutine(_loadingLoginSceneCo);

            _loadingLoginSceneCo = StartCoroutine(LoadLoginSceneCo());
        }

        private IEnumerator LoadLoginSceneCo()
        {
            yield return new WaitForSeconds(loadingLoginSceneDelay);
            ChangeScene(Scene.Login);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Change Scene with Photon LoadLevel
        /// </summary>
        /// <param name="newScene">enum Scene</param>
        public static void ChangeScene(Scene newScene)
        {
            PhotonNetwork.LoadLevel(newScene.ToString());
        }

        #endregion
    }
}
