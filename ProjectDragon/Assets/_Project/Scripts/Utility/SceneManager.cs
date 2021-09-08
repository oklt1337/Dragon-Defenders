using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace _Project.Scripts.Utility
{
    public enum Scene
    {
        Intro,
        Login,
        MainMenu,
        Lobby,
        InGame
    }
    
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private float loadingLoginSceneDelay = 3f;

        private Coroutine _loadingLoginSceneCo;

        public static Scene CurrentScene => GetCurrentScene();
        
        private void Start()
        {
            LoadLoginScene();
        }

        private static Scene GetCurrentScene()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex switch
            {
                0 => Scene.Intro,
                1 => Scene.Login,
                2 => Scene.MainMenu,
                3 => Scene.Lobby,
                4 => Scene.InGame,
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

        /// <summary>
        /// Change Scene with Photon LoadLevel
        /// </summary>
        /// <param name="newScene">enum Scene</param>
        public static void ChangeScene(Scene newScene)
        {
            PhotonNetwork.LoadLevel(newScene.ToString());
        }
    }
}
