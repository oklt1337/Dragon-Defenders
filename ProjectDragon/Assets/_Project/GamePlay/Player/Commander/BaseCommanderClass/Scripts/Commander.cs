using System;
using System.Collections.Generic;
using System.Linq;
using Abilities.Ability.Scripts;
using Abilities.EndAbilities.IncreaseDamageForSetTime.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using Abilities.VisitorPattern.Scripts;
using Deck_Cards.Cards.CommanderCard.Scripts;
using GamePlay.GameManager.Scripts;
using GamePlay.Player.PlayerModel.Scripts;
using SkillSystem.SkillTree.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Player.Commander.BaseCommanderClass.Scripts
{
    public class Commander : MonoBehaviour
    {
        #region SerializeFields
        
        [SerializeField] private Animator animator;

        #endregion

        #region Private Fields

        [Header("PlayerModel")] 
        private PlayerModel.Scripts.PlayerModel playerModel;

        [Header("Basic")] 
        private string commanderName;

        [Header("Stats")]
        private CommanderStats.Scripts.CommanderStats commanderStats;
        private SkillTree skillTree;
        private List<Ability> abilities = new List<Ability>();
        private readonly Client client = new Client();

        [Header("Runtime")]
        private bool dyingBreath;
        private byte rank;
        private byte level;
        private float experience;
        private const float MINDamage = 10f;

        [Header("Movement")] 
        private NavMeshAgent navMeshAgent;
        private Coroutine movementCo;
        private Vector3 destination;

        #endregion

        #region Public Properties

        internal NavMeshAgent NavMeshAgent
        {
            set => navMeshAgent = value;
        }

        public string CommanderName
        {
            get => commanderName;
            private set => commanderName = value;
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

        #region Unity Methods

        private void Awake()
        {
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += StopMovement;
            CommanderStats.OnCommanderSpeedChanged += speed => navMeshAgent.speed = speed;
        }

        private void Update()
        {
            if (playerModel.CurrentState != State.Move) 
                return;
            var dist=navMeshAgent.remainingDistance;
            if (!float.IsPositiveInfinity(dist) && navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                navMeshAgent.remainingDistance == 0)
                playerModel.ChangeState(State.Idle);
        }

        #endregion

        #region Private Methods

        private void SetStats(CommanderCard commanderCard)
        {
            commanderName = commanderCard.CardName;
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
                        var damageAbility = damageAbilityObj.CreateInstance<DamageAbility>();
                        damageAbility.Init(transform);
                        abilities.Add(damageAbility);
                        client.Visitors.Add(damageAbility);
                        break;
                    case AbilityType.Utility:
                        var utilityAbilityObj = (UtilityAbilityObj) ability;
                        var utilityAbility = utilityAbilityObj.CreateInstance<UtilityAbility>();
                        utilityAbility.Init(transform);
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

        private void Cast(int index, Transform target)
        {
            if (abilities[index].AbilityAbilityObj.AbilityType == AbilityType.Damage)
            {
                ((DamageAbility) abilities[index]).Cast(transform, target, Caster.Commander);
            }
            else
            {
                if (abilities[index] is IncreaseDamageForSetTimeAbility)
                {
                    var units = GameManager.Scripts.GameManager.Instance.UnitManager.Units;

                    foreach (var unit in units
                        .Where(unit =>
                            Vector3.Distance(unit.transform.position, transform.position) <=
                            ((IncreaseDamageForSetTimeAbility) abilities[index]).EffectRange).Where(unit =>
                            unit.Ability.AbilityAbilityObj.AbilityType == AbilityType.Damage))
                    {
                        ((IncreaseDamageForSetTimeAbility) abilities[index]).OnStay(unit.transform);
                    }
                }

                ((UtilityAbility) abilities[index]).OnStay(target);
            }
        }

        #endregion

        #region Protected Methods

        internal void InitializeCommander(CommanderCard commanderCard)
        {
            SetStats(commanderCard);
        }

        internal void Initialize(PlayerModel.Scripts.PlayerModel model)
        {
            playerModel = model;
        }

        #endregion

        #region Public Methods

        public void Move(Vector3 moveTo)
        {
            if (playerModel.CurrentState == State.Blocked)
                return;
            
            if (navMeshAgent.SetDestination(moveTo))
            {
                playerModel.ChangeState(State.Move);
                destination = moveTo;
                GameManager.Scripts.GameManager.Instance.CommanderMoveIndicator.InitializeMovePoint(destination);
            }
            else
            {
                destination = Vector3.zero;
            }
        }

        public void AutoAttack(Transform target)
        {
            Cast(0, target);
            playerModel.ChangeState(State.AutoAttack);
        }

        public void Attack1(Transform target)
        {
            Cast(1, target);
            playerModel.ChangeState(State.Attack1);
        }

        public void Attack2(Transform target)
        {
            Cast(2, target);
            playerModel.ChangeState(State.Attack2);
        }

        public void Attack3(Transform target)
        {
            Cast(3, target);
            playerModel.ChangeState(State.Attack3);
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
        }

        public void AddExp(float gainedExp)
        {
            experience += gainedExp;
        }

        #endregion
    }
}