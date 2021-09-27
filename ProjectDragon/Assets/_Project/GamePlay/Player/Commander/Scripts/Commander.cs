using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Enemies;
using _Project.Scripts.Gameplay.Faction;
using _Project.Scripts.Gameplay.Skillsystem;
using _Project.Scripts.Gameplay.Skillsystem.Ability;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using UnityEngine;

namespace _Project.GamePlay.Player.Commander.Scripts
{
    public class Commander : MonoBehaviour
    {
        #region SerializeFields

        #endregion

        #region Private Fields

        private string _commanderName;
        private GameObject _commanderObj;
        private Factions.Faction _faction;
        private Factions.Class _commanderClass;
        private float _health;
        private float _mana;
        private float _attackDamageModifier;
        private float _defense;
        private float _speed;
        private byte _rank;
        private byte _level;
        private float _experience;
        private PointAndClickDamageAbility _primaryAttack;
        private SkillTree _skillTree;
        private List<Ability> _abilities;
        private Animator _animator;
        
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
            private set => _mana = value;
        }

        public float AttackDamageModifier
        {
            get => _attackDamageModifier;
            private set => _attackDamageModifier = value;
        }

        public float Defense
        {
            get => _defense;
            private set => _defense = value;
        }

        public float Speed
        {
            get => _speed;
            private set => _speed = value;
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
            private set => _abilities = value;
        }

        public Animator Animator
        {
            get => _animator;
            set => _animator = value;
        }

        #endregion

        #region Events

        public event Action OnDeath;

        #endregion

        #region Unity Methods

        #endregion

        #region Private Methods

        private IEnumerator LerpMovementCo(Vector3 moveTo)
        {
            while (transform.position != moveTo)
            {
                transform.position = Vector3.MoveTowards(transform.position, moveTo, _speed * Time.deltaTime);
                
                yield return null;
            }
        }

        #endregion

        #region Protected Methods

        internal void Move(Vector3 moveTo)
        {
            moveTo.y = transform.position.y;

            if(_movementCo != null)
                StopCoroutine(_movementCo);
            
            _movementCo = StartCoroutine(LerpMovementCo(moveTo));
        }
        
        internal void SetStats(CommanderModel.Scripts.CommanderModel commanderModel)
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
            _primaryAttack = commanderModel.primaryAttack;
            _skillTree = commanderModel.skillTree;
            _abilities = commanderModel.abilities;
            _animator = commanderModel.animator;
        }

        #endregion

        #region Public Methods

        public void TakeDamage(float dmg)
        {
            _health -= dmg;

            if (!(_health <= 0)) 
                return;
            
            _health = 0;
            OnDeath?.Invoke();
        }

        #endregion
    }
}
