using System;
using System.Collections;
using _Project.Network.NetworkManager.Scripts;
using _Project.Network.PlayFab.Scripts;
using Photon.Pun;
using UnityEngine;

namespace _Project.Utility.SceneManager.Scripts
{
    public enum Scene
    {
        Intro,
        Authorize,
        MainMenu,
        Lobby,
        GameScene
    }
    
    public class SceneManager : MonoBehaviour
    {
        #region Public Properties

        public static Scene CurrentScene => GetCurrentScene();

        #endregion

        #region Private Methods

        /// <summary>
        /// Get Current Scene.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        private static Scene GetCurrentScene()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex switch
            {
                0 => Scene.Intro,
                1 => Scene.Authorize,
                2 => Scene.MainMenu,
                3 => Scene.Lobby,
                4 => Scene.GameScene,
                _ => throw new IndexOutOfRangeException()
            };
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
