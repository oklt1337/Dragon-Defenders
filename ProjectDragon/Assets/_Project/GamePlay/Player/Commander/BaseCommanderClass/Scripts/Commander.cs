using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Faction;
using _Project.GamePlay.GameManager.Scripts;
using _Project.SkillSystem.SkillTree;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.GamePlay.Player.Commander.BaseCommanderClass.Scripts
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

        private string commanderName;
        private GameObject commanderObj;
        private ClassAndFaction.Faction faction;
        private ClassAndFaction.Class commanderClass;
        private float health;
        private float maxHealth;
        private float mana;
        private float attackDamageModifier;
        private float defense;
        private float speed;
        private bool dyingBreath;
        private byte rank;
        private byte level;
        private float experience;
        private SkillTree skillTree;
        private List<Ability> abilities = new List<Ability>();
        private const float MINDamage = 10f;
        private Vector3 destination;
        private State currentState;

        private Coroutine movementCo;

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

        public ClassAndFaction.Faction Faction
        {
            get => faction;
            private set => faction = value;
        }

        public ClassAndFaction.Class CommanderClass
        {
            get => commanderClass;
            private set => commanderClass = value;
        }

        public float MAXHealth
        {
            get => maxHealth;
            internal set => maxHealth = value;
        }

        public float Health
        {
            get => health;
            private set
            {
                health = value;
                if (health <= 0)
                {
                    OnDeath?.Invoke();
                }
            }
        }

        public float Mana
        {
            get => mana;
            internal set => mana = value;
        }

        public float AttackDamageModifier
        {
            get => attackDamageModifier;
            internal set => attackDamageModifier = value;
        }

        public float Defense
        {
            get => defense;
            internal set => defense = value;
        }

        public float Speed
        {
            get => speed;
            internal set => speed = value;
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

        private void SetStats(CommanderModel.Scripts.CommanderModel commanderModel)
        {
            commanderName = commanderModel.commanderName;
            commanderObj = commanderModel.commanderObj;
            faction = commanderModel.faction;
            commanderClass = commanderModel.commanderClass;
            health = commanderModel.health;
            mana = commanderModel.mana;
            attackDamageModifier = commanderModel.attackDamageModifier;
            defense = commanderModel.defense;
            speed = commanderModel.speed;
            rank = commanderModel.rank;
            level = commanderModel.level;
            experience = commanderModel.experience;
            skillTree = commanderModel.skillTree;
            navMeshAgent.speed = speed;

            foreach (Ability ability in commanderModel.commanderAbilityDataBase.CommanderAbilitiesScripts.Select(type =>
                (Ability)gameObject.AddComponent(type)))
            {
                abilities.Add(ability);
            }

            for (int i = 0; i < commanderModel.commanderAbilityDataBase.commanderAbilitiesDataBases.Count; i++)
            {
                abilities[i].Init(commanderModel.commanderAbilityDataBase.commanderAbilitiesDataBases[i]);
            }
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

        internal void InitializeCommander(CommanderModel.Scripts.CommanderModel commanderModel)
        {
            SetStats(commanderModel);
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
            abilities[0].Cast(transform, target.transform);
        }

        public void TakeDamage(float damage)
        {
            damage = Mathf.Clamp((damage * health / maxHealth), MINDamage, damage) / defense;

            if (health - damage <= 0 && !dyingBreath)
            {
                health = 1f;
                dyingBreath = true;
            }
            else
            {
                health -= damage;
                dyingBreath = false;
            }

            if (!(health <= 0) || dyingBreath)
                return;

            health = 0;
            OnDeath?.Invoke();
        }

        #endregion
    }
}
