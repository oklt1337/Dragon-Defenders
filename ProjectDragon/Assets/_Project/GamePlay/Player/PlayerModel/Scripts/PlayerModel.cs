using System;
using Deck_Cards.Decks.Scripts;
using GamePlay.GameManager.Scripts;
using Photon.Pun;
using SkillSystem.SkillTree.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Unit = Units.Unit.BaseUnits.Unit;

namespace GamePlay.Player.PlayerModel.Scripts
{
    public enum State
    {
        Idle,
        Move,
        Blocked
    }
    public class PlayerModel : MonoBehaviour
    {
        #region SerializeFields
        
        [Header("State")]
        [SerializeField] private State currentState;
        
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
        

        #region Public Properties

        public Commander.BaseCommanderClass.Scripts.Commander Commander => commander;
        public InputHandler.Scripts.InputHandler InputHandler => inputHandler;
        public State CurrentState => currentState;

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
        public event Action<State> OnPlayerStateChanged;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            inputHandler.OnTouch += ProcessInput;
            GameManager.Scripts.GameManager.Instance.OnDeckSet += Initialize;
        }

        private void Start()
        {
            transform.position = GameManager.Scripts.GameManager.Instance.PlayerSpawn.position;
            inputHandler.CommanderCam =  GameManager.Scripts.GameManager.Instance.CommanderCamera;
        }

        #endregion

        #region Private Methods

        private void ProcessInput(Ray ray)
        {
            if (!Physics.Raycast(ray, out var hit))
                return;

            if (hit.collider.CompareTag("Enemy") &&
                GameManager.Scripts.GameManager.Instance.CurrentGameState == GameState.Wave)
            {
                commander.AutoAttack(hit.transform);
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
        
        private void Initialize(Deck deck)
        {
     //      var fullPath = AssetDatabase.GetAssetPath(deck.CommanderCard.Model.GetInstanceID());
     //      fullPath = fullPath.Remove(0, 17);
     //      var path = fullPath.Split('.');
     //      var myTransform = transform;

     //      var commanderObj = PhotonNetwork.Instantiate(path[0], myTransform.position, Quaternion.identity);
     //      commander = commanderObj.GetComponent<Commander.BaseCommanderClass.Scripts.Commander>();
     //      commander.transform.parent = myTransform;
     //      commander.NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
     //      commander.InitializeCommander(deck.CommanderCard);
     //      
     //      //Init Rest
     //      inputHandler.Initialize(this);
     //      commander.Initialize(this);
     //      animationHandler.Animator = commander.Animator;
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

        public void ChangeState(State newState)
        {
            currentState = newState;
            OnPlayerStateChanged?.Invoke(currentState);
        }

        #endregion
    }
}