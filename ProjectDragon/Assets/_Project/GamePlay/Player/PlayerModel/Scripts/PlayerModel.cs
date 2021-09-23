using System;
using _Project.GamePlay.GameManager.Scripts;
using UnityEngine;

namespace _Project.GamePlay.Player.PlayerModel.Scripts
{
    public class PlayerModel : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private Camera buildCamera;
        [SerializeField] private Camera commanderCamera;

        #endregion

        #region Private Fields

        private int _money;
        private Commander.Scripts.Commander _commander;

        #endregion

        #region Protected Fields

        

        #endregion

        #region Public Fields

        

        #endregion

        #region Public Properties

        

        #endregion

        #region Unity Methods

        private void Awake()
        {
            //change camera event 
        }

        #endregion

        #region Private Methods

        private void ChangeCamera(GameState state)
        {
            switch (state)
            {
                case GameState.Build:
                    buildCamera.gameObject.SetActive(true);
                    commanderCamera.gameObject.SetActive(false);
                    break;
                case GameState.Wave:
                    buildCamera.gameObject.SetActive(false);
                    commanderCamera.gameObject.SetActive(true);
                    break;
                case GameState.Prepare:
                    Debug.LogError($"GameState: {state}");
                    break;
                case GameState.End:
                    Debug.LogError($"GameState: {state}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        #endregion

        #region Protected Methods

        

        #endregion

        #region Public Methods

        

        #endregion
    }
}
