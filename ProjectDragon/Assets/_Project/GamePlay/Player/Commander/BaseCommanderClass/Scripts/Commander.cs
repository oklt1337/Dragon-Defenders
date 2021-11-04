using System;
using System.Collections.Generic;
using System.Linq;
using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using AI.Enemies.Base_Enemy;
using Deck_Cards.Cards.CommanderCard.Scripts;
using Faction;
using GamePlay.GameManager.Scripts;
using SkillSystem.SkillTree.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Player.Commander.BaseCommanderClass.Scripts
{
    public class Commander : MonoBehaviour
    {
        public enum State
        {
            Idle,
            Move
        }

        #region SerializeFields

        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;

        #endregion

        #region Private Fields

        [Header("Basic")] private string commanderName;
        private GameObject commanderObj;

        [Header("Stats")] private CommanderStats.Scripts.CommanderStats commanderStats;
        private SkillTree skillTree;
        private List<Ability> abilities = new List<Ability>();
        private Client client;

        [Header("Runtime")] private bool dyingBreath;
        private byte rank;
        private byte level;
        private float experience;
        private State currentState;
        private const float MINDamage = 10f;

        [Header("Movement")] private Coroutine movementCo;
        private Vector3 destination;

        #endregion

        #region Public Properties

        public string CommanderName
        {
            get => commanderName;
            private set => commanderName = value;
        }

        public GameObject CommanderObj
        {
            get => commanderObj;
            private set => commanderObj = value;
        }

        public CommanderStats.Scripts.CommanderStats CommanderStats
        {
            get => commanderStats;
            private set => commanderStats = value;
        }

        public byte Rank
        {
            get => rank;
            private set => rank = value;
        }

        public byte Level
        {
            get => level;
            private set => level = value;
        }

        public float Experience
        {
            get => experience;
            private set => experience = value;
        }

        public SkillTree SkillTree
        {
            get => skillTree;
            private set => skillTree = value;
        }

        public List<Ability> Abilities
        {
            get => abilities;
            internal set => abilities = value;
        }

        public Animator Animator
        {
            get => animator;
            set => animator = value;
        }

        #endregion

        #region Events

        public event Action OnDeath;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            currentState = State.Idle;
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += StopMovement;
        }

        #endregion

        #region Private Methods

        private void SetStats(CommanderCard commanderCard)
        {
            commanderName = commanderCard.CardName;
            commanderObj = commanderCard.Model;

            commanderStats = new CommanderStats.Scripts.CommanderStats(commanderCard.Faction, commanderCard.Class,
                commanderCard.Health, commanderCard.Mana, commanderCard.AttackDamageModifier, commanderCard.Defense,
                commanderCard.Speed);
            client.Visitors.Add(commanderStats);

            foreach (var ability in commanderCard.abilityDataBase.Abilities)
            {
                switch (ability.AbilityType)
                {
                    case AbilityType.Damage:
                        var damageAbilityObj = (DamageAbilityObj) ability;
                        var damageAbility = damageAbilityObj.CreateInstance();
                        abilities.Add(damageAbility);
                        client.Visitors.Add(damageAbility);
                        break;
                    case AbilityType.Utility:
                        var utilityAbilityObj = (UtilityAbilityObj) ability;
                        var utilityAbility = utilityAbilityObj.CreateInstance();
                        abilities.Add(utilityAbility);
                        client.Visitors.Add(utilityAbility);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            skillTree = commanderCard.SkillTreeObj.CreateInstance(client);
        }

        private void StopMovement(GameState state)
        {
            navMeshAgent.isStopped = state switch
            {
                GameState.Build => true,
                GameState.Wave => false,
                _ => navMeshAgent.isStopped
            };
        }

        #endregion

        #region Protected Methods

        internal void InitializeCommander(CommanderCard commanderCard)
        {
            SetStats(commanderCard);
        }

        #endregion

        #region Public Methods

        public void Move(Vector3 moveTo)
        {
            if (navMeshAgent.SetDestination(moveTo))
            {
                destination = moveTo;
                GameManager.Scripts.GameManager.Instance.CommanderMoveIndicator.InitializeMovePoint(destination);
                currentState = State.Move;
            }
            else
            {
                destination = Vector3.zero;
            }
        }

        public void Attack(Component target)
        {
            Debug.Log(target.name);
            //abilities[0].AbilityObj.Cast(transform, target.transform);
            target.gameObject.GetComponent<Enemy>().TakeDamage(10);
        }

        public void TakeDamage(float damage)
        {
            damage = Mathf.Clamp((damage * commanderStats.Health / commanderStats.MAXHealth), MINDamage, damage) /
                     commanderStats.Defense;

            if (commanderStats.Health - damage <= 0 && !dyingBreath)
            {
                commanderStats.Health = 1f;
                dyingBreath = true;
            }
            else
            {
                commanderStats.Health -= damage;
                dyingBreath = false;
            }

            if (!(commanderStats.Health <= 0) || dyingBreath)
                return;

            commanderStats.Health = 0;
            OnDeath?.Invoke();
        }

        public void AddExp(float gainedExp)
        {
            experience += gainedExp;
        }

        #endregion
    }
}