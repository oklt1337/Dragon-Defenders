using System;
using _Project.GamePlay.GameManager.Scripts;
using _Project.GamePlay.Player.Commander.Scripts;
using _Project.GamePlay.Player.CommanderModel.Library;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

namespace _Project.GamePlay.Player.PlayerModel.Scripts
{
    public class PlayerModel : MonoBehaviour
    {
        #region SerializeFields

        [Header("Commander")] [SerializeField] private Commander.Scripts.Commander commander;

        [Header("Cameras")] [SerializeField] private Camera buildCamera;
        [SerializeField] private Camera commanderCamera;

        [Header("Handler")] [SerializeField] private AnimationHandler animationHandler;
        [SerializeField] private SoundHandler soundHandler;

        [SerializeField] private InputHandler inputHandler;
        //[SerializeField] private CollisionHandler CollisionHandler;

        #endregion

        #region Private Fields

        private int _money;

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
            InitializeCommander();
        }

        private void Start()
        {
            inputHandler.OnTouch += commander.Move;
            inputHandler.CommanderCam = commanderCamera;
            animationHandler.Animator = commander.Animator;
        }

        #endregion

        #region Private Methods

        private void InitializeCommander()
        {
            /*Hashtable hashTable = PhotonNetwork.LocalPlayer.CustomProperties;
            if (!hashTable.ContainsKey("Commander"))
                return;*/
           CommanderModel.Scripts.CommanderModel commanderModel = GameManager.Scripts.GameManager.Instance.CommanderLibrary.CommanderModels[Commanders.Commander1];
           commander.SetStats(commanderModel);
        }

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
