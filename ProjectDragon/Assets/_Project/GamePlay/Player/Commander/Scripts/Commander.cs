using System;
using System.Collections.Generic;
using _Project.Scripts.Animation;
using _Project.Scripts.Gameplay.Faction;
using _Project.Scripts.Gameplay.Skillsystem;
using _Project.Scripts.Gameplay.Skillsystem.Ability;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using _Project.Scripts.Sound;
using UnityEngine;

namespace _Project.GamePlay.Player.Commander.Scripts
{
    public class Commander : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private string commanderName;
        [SerializeField] private GameObject commanderModel;
        [SerializeField] private Factions.Faction faction;
        [SerializeField] private Factions.Class commanderClass;
        [SerializeField] private float health;
        [SerializeField] private float mana;
        [SerializeField] private float attackDamageModifier;
        [SerializeField] private float defense;
        [SerializeField] private float speed;
        [SerializeField] private byte rank;
        [SerializeField] private byte level;
        [SerializeField] private float experience;
        [SerializeField] private PointAndClickDamageAbility primaryAttack;
        [SerializeField] private SkillTree skillTree;
        [SerializeField] private List<Ability> abilities;
        [SerializeField] private AnimationHandler animationHandler;
        [SerializeField] private SoundHandler soundHandler;
        //[SerializeField] private InputHandler InputHandler;
        //[SerializeField] private CollisionHandler CollisionHandler;

        #endregion

        #region Private Fields

        

        #endregion

        #region Protected Fields

        

        #endregion
        
        #region Public Fields

        

        #endregion

        #region Public Properties

        public string CommanderName
        {
            get => commanderName;
            private set => commanderName = value;
        }

        public GameObject CommanderModel
        {
            get => commanderModel;
            private set => commanderModel = value;
        }

        public Factions.Faction Faction
        {
            get => faction;
            private set => faction = value;
        }

        public Factions.Class CommanderClass
        {
            get => commanderClass;
            private set => commanderClass = value;
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
            private set => mana = value;
        }

        public float AttackDamageModifier
        {
            get => attackDamageModifier;
            private set => attackDamageModifier = value;
        }

        public float Defense
        {
            get => defense;
            private set => defense = value;
        }

        public float Speed
        {
            get => speed;
            private set => speed = value;
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

        public PointAndClickDamageAbility PrimaryAttack
        {
            get => primaryAttack;
            private set => primaryAttack = value;
        }

        public SkillTree SkillTree
        {
            get => skillTree;
            private set => skillTree = value;
        }

        public List<Ability> Abilities
        {
            get => abilities;
            private set => abilities = value;
        }

        public AnimationHandler AnimationHandler
        {
            get => animationHandler;
            private set => animationHandler = value;
        }

        public SoundHandler SoundHandler
        {
            get => soundHandler;
            private set => soundHandler = value;
        }

        #endregion

        #region Events

        public event Action OnDeath;

        #endregion

        #region Unity Methods

        

        #endregion

        #region Private Methods

        private void ChangeAnimation(AnimationClip animationClip)
        {
            
        }

        #endregion

        #region Protected Methods

        

        #endregion

        #region Public Methods

        public void TakeDamage(float dmg)
        {
            health -= dmg;
        }

        #endregion
    }
}
