using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.AI.Enemies.Scripts;
using _Project.Scripts.Gameplay.Faction;
using _Project.Scripts.Gameplay.Skillsystem;
using _Project.Scripts.Gameplay.Skillsystem.Ability;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using _Project.SkillSystem.SkillTree;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.GamePlay.Player.Commander.BaseCommanderClass.Scripts
{
    public enum State
    {
        Idle,
        Move
    }
    
    public class Commander : MonoBehaviour
    {
        #region SerializeFields
        
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;
        
        #endregion

        #region Private Fields

        private string _commanderName;
        private GameObject _commanderObj;
        private Factions.Faction _faction;
        private Factions.Class _commanderClass;
        private float _health;
        private float _maxHealth;
        private float _mana;
        private float _attackDamageModifier;
        private float _defense;
        private float _speed;
        private bool _dyingBreath;
        private byte _rank;
        private byte _level;
        private float _experience;
        private PointAndClickDamageAbility _primaryAttack;
        private SkillTree _skillTree;
        private List<Ability> _abilities;
        private AnimatorController _animatorController;
        private const float MINDamage = 10f;
        private Vector3 _destination;
        private State _currentState;

        private Coroutine _movementCo;

        #endregion

        #region Protected Fields

        

        #endregion
        
        #region Public Fields

        

        #endregion

        #region Public Properties

        public string CommanderName
        {
            get => _commanderName;
            private set => _commanderName = value;
        }

        public GameObject CommanderObj
        {
            get => _commanderObj;
            private set => _commanderObj = value;
        }

        public Factions.Faction Faction
        {
            get => _faction;
            private set => _faction = value;
        }

        public Factions.Class CommanderClass
        {
            get => _commanderClass;
            private set => _commanderClass = value;
        }
        
        public float MAXHealth
        {
            get => _maxHealth;
            internal set => _maxHealth = value;
        }

        public float Health
        {
            get => _health;
            private set
            {
                _health = value;
                if (_health <= 0)
                {
                    OnDeath?.Invoke();
                }
            } 
        }

        public float Mana
        {
            get => _mana;
            internal set => _mana = value;
        }

        public float AttackDamageModifier
        {
            get => _attackDamageModifier;
            internal set => _attackDamageModifier = value;
        }

        public float Defense
        {
            get => _defense;
            internal set => _defense = value;
        }

        public float Speed
        {
            get => _speed;
            internal set => _speed = value;
        }

        public byte Rank
        {
            get => _rank;
            private set => _rank = value;
        }

        public byte Level
        {
            get => _level;
            private set => _level = value;
        }

        public float Experience
        {
            get => _experience;
            private set => _experience = value;
        }

        public PointAndClickDamageAbility PrimaryAttack
        {
            get => _primaryAttack;
            private set => _primaryAttack = value;
        }

        public SkillTree SkillTree
        {
            get => _skillTree;
            private set => _skillTree = value;
        }

        public List<Ability> Abilities
        {
            get => _abilities;
            internal set => _abilities = value;
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
            _currentState = State.Idle;
        }

        #endregion

        #region Private Methods

        private void SetStats(CommanderModel.Scripts.CommanderModel commanderModel)
        {
            _commanderName = commanderModel.commanderName;
            _commanderObj = commanderModel.commanderObj;
            _faction = commanderModel.faction;
            _commanderClass = commanderModel.commanderClass;
            _health = commanderModel.health;
            _mana = commanderModel.mana;
            _attackDamageModifier = commanderModel.attackDamageModifier;
            _defense = commanderModel.defense;
            _speed = commanderModel.speed;
            _rank = commanderModel.rank;
            _level = commanderModel.level;
            _experience = commanderModel.experience;
            _skillTree = commanderModel.skillTree;
            _animatorController = commanderModel.animatorController;
            navMeshAgent.speed = _speed;
        }

        public void Move(Vector3 moveTo)
        {
            if (navMeshAgent.SetDestination(moveTo))
            {
                _destination = moveTo;
                GameManager.Scripts.GameManager.Instance.CommanderMoveIndicator.InitializeMovePoint(_destination);
                _currentState = State.Move;
            }
            else
            {
                _destination = Vector3.zero;
            }
        }

        public void Attack(Component target)
        {
            Debug.Log(target.name);
            _primaryAttack.Cast( transform,target.transform);
        }
        
        #endregion

        #region Protected Methods

        internal void InitializeCommander(CommanderModel.Scripts.CommanderModel commanderModel)
        {
            SetStats(commanderModel);
        }

        #endregion

        #region Public Methods

        public void TakeDamage(float damage)
        {
            damage = Mathf.Clamp((damage * _health / _maxHealth), MINDamage, damage) / _defense;

            if (_health - damage <= 0 && !_dyingBreath)
            {
                _health = 1f;
                _dyingBreath = true;
            }
            else
            {
                _health -= damage;
                _dyingBreath = false;
            }

            if (!(_health <= 0) || _dyingBreath) 
                return;
            
            _health = 0;
            OnDeath?.Invoke();
        }

        #endregion
    }
}
