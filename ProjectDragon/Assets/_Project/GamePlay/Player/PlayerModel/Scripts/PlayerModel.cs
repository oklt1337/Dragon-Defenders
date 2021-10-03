using System;
using _Project.GamePlay.GameManager.Scripts;
using _Project.GamePlay.Player.Commander.CommanderModel.CLibrary;
using _Project.GamePlay.Player.Commander.CommanderModel.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace _Project.GamePlay.Player.PlayerModel.Scripts
{
    public class PlayerModel : MonoBehaviour
    {
        #region SerializeFields

        [Header("Commander")] 
        [SerializeField] private Commander.BaseCommanderClass.Scripts.Commander commander;

        [Header("Cameras")] 
        [SerializeField] private Camera buildCamera;
        [SerializeField] private Camera commanderCamera;

        [Header("Handler")] 
        [SerializeField] private AnimationHandler.Scripts.AnimationHandler animationHandler;
        [SerializeField] private SoundHandler.Scripts.SoundHandler soundHandler;
        [SerializeField] private InputHandler.Scripts.InputHandler inputHandler;
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

        public Commander.BaseCommanderClass.Scripts.Commander Commander => commander;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += ChangeCamera;
            inputHandler.OnTouch += ProcessInput;
            Initialize();
        }

        private void Start()
        {
            inputHandler.CommanderCam = commanderCamera;
            animationHandler.Animator = commander.Animator;
        }

        #endregion

        #region Private Methods

        private void ProcessInput(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    commander.Attack(hit.transform);
                }
                else if (hit.collider.CompareTag("Ground"))
                {
                    commander.Move(hit.point);
                }
            }
        }

        private void Initialize()
        {
            /*Hashtable hashTable = PhotonNetwork.LocalPlayer.CustomProperties;
            if (!hashTable.ContainsKey("Commander"))
                return;*/
           CommanderModel commanderModel = GameManager.Scripts.GameManager.Instance.CommanderLibrary.CommanderModels[Commanders.Commander1];
           commander.InitializeCommander(commanderModel);
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
                    buildCamera.gameObject.SetActive(false);
                    commanderCamera.gameObject.SetActive(false);
                    Debug.LogError($"GameState: {state}");
                    break;
                case GameState.End:
                    buildCamera.gameObject.SetActive(false);
                    commanderCamera.gameObject.SetActive(true);
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

        public void Build(int cost)
        {
            if (_money <= cost)
                return;
            _money -= cost;
            
            //Build stuff
        }
        
        public void AddMoney(int amount)
        {
            _money += amount;
        }
        
        #endregion
    }
}
