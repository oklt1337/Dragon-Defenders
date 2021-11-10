using System;
using Deck_Cards.Cards.CommanderCard.Scripts;
using GamePlay.GameManager.Scripts;
using Photon.Pun;
using SkillSystem.SkillTree.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace GamePlay.Player.PlayerModel.Scripts
{
    /// <summary>
    /// Author: Christopher Zelch
    /// </summary>
    public class PlayerModel : MonoBehaviour
    {
        #region SerializeFields

        [Header("Commander")] 
        [SerializeField] private Commander.BaseCommanderClass.Scripts.Commander commander;

        

        [Header("Handler")] 
        [SerializeField] private AnimationHandler.Scripts.AnimationHandler animationHandler;
        [SerializeField] private SoundHandler.Scripts.SoundHandler soundHandler;
        [SerializeField] private InputHandler.Scripts.InputHandler inputHandler;

        //TODO: ColliderHandler adden
        //[SerializeField] private CollisionHandler CollisionHandler;

        #endregion

        #region Private Fields

        private int money;

        #endregion

        #region Protected Fields

        #endregion

        #region Public Fields

        #endregion

        #region Public Properties

        public Commander.BaseCommanderClass.Scripts.Commander Commander => commander;
        public InputHandler.Scripts.InputHandler InputHandler => inputHandler;

        public int Money
        {
            get => money;
            private set
            {
                if (value < 0)
                {
                    value = 0;
                }

                money = value;
            }
        }

        #endregion

        #region Events

        public event Action<SkillTree> OnTryUpgradeSkill;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            inputHandler.OnTouch += ProcessInput;
            Initialize();
        }

        private void Start()
        {
            transform.position = GameManager.Scripts.GameManager.Instance.PlayerSpawn.position;
            inputHandler.CommanderCam =  GameManager.Scripts.GameManager.Instance.CommanderCamera;
            animationHandler.Animator = commander.Animator;
        }

        #endregion

        #region Private Methods

        private void ProcessInput(Ray ray)
        {
            if (!Physics.Raycast(ray, out RaycastHit hit))
                return;

            if (hit.collider.CompareTag("Enemy") &&
                GameManager.Scripts.GameManager.Instance.CurrentGameState == GameState.Wave)
            {
                commander.Attack(hit.transform);
            }
            else if (hit.collider.CompareTag("Ground") &&
                     GameManager.Scripts.GameManager.Instance.CurrentGameState == GameState.Wave)
            {
                commander.Move(hit.point);
            }
            else if (hit.collider.CompareTag("Unit/Tower") &&
                     GameManager.Scripts.GameManager.Instance.CurrentGameState == GameState.Build)
            {
                var unit = hit.collider.gameObject.GetComponent<Unit>();
                if (unit != null)
                {
                    OnTryUpgradeSkill?.Invoke(unit.SkillTree);
                }
            }
            else if (hit.collider.CompareTag("Player") &&
                     GameManager.Scripts.GameManager.Instance.CurrentGameState == GameState.Build)
            {
                OnTryUpgradeSkill?.Invoke(commander.SkillTree);
            }
        }

        private void Initialize()
        {
            var hashTable = PhotonNetwork.LocalPlayer.CustomProperties;
            if (!hashTable.ContainsKey("Commander"))
                return;
            
            var commanderCard = (CommanderCard) hashTable["Commander"];
            commander.InitializeCommander(commanderCard);
        }

        #endregion

        #region Protected Methods

        #endregion

        #region Public Methods

        /// <summary>
        /// Amount will be added onto Money
        /// </summary>
        /// <param name="amount">int</param>
        /// <returns>true if money is more then 0 after modification</returns>
        public bool ModifyMoney(int amount)
        {
            if (Money + amount < 0)
            {
                return false;
            }

            Money += amount;
            return true;
        }

        #endregion
    }
}