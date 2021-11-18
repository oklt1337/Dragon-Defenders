using System;
using System.Collections.Generic;
using System.Linq;
using Abilities.Ability.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using Abilities.VisitorPattern.Scripts;
using AI.Enemies.Base_Enemy.Scripts;
using Deck_Cards.Cards.BaseCards.Scripts;
using Deck_Cards.Cards.UnitCard.Scripts;
using Faction;
using SkillSystem.SkillTree.Scripts;
using UnityEngine;

namespace Units.Unit.BaseUnits
{
    public enum AttackOrder
    {
        First,
        Last,
        Closest,
        Strongest,
        Weakest,
        HighestHealth,
        LowestHealth
    }
    
    [RequireComponent(typeof(SphereCollider))]
    public sealed class Unit : MonoBehaviour, IVisitor
    {
        #region SerializeFields

        [SerializeField] private Transform spawnPos;
        [SerializeField] private Animator animator;
        [SerializeField] private SphereCollider sphereCollider;

        #endregion

        #region Private Fields

        private List<Transform> enteredEnemies = new List<Transform>();
        private AttackOrder attackOrder;
        private ClassAndFaction.Faction faction;
        private ClassAndFaction.Class unitClass;
        private Client client;

        #endregion

        #region Public Propeties

        public Animator Animator
        {
            get => animator;
            set => animator = value;
        }

        public ClassAndFaction.Faction Faction
        {
            get => faction;
            set => faction = value;
        }

        public ClassAndFaction.Class UnitClass
        {
            get => unitClass;
            set => unitClass = value;
        }

        public SkillTree SkillTree { get; private set; }

        public Ability Ability { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            client = new Client();
            attackOrder = AttackOrder.First;

            if (sphereCollider == null)
            {
                sphereCollider = GetComponent<SphereCollider>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (TriggerReturns(other))
            {
                enteredEnemies.Add(other.transform);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (enteredEnemies.Count == 0)
                return;
            enteredEnemies = enteredEnemies.Where(enemy => enemy != null).ToList();
            if (!TriggerReturns(other)) 
                return;
            
            SelectTarget();
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (TriggerReturns(other))
            {
                enteredEnemies.Remove(other.transform);
            }
        }

        private void Update()
        {
            if (Ability == null)
                return;
            if (!Ability.StartCooldown && !(Ability.TimeLeft > 0)) 
                return;
            Ability.Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (Ability.AbilityAbilityObj.AbilityType == AbilityType.Utility)
                Cast(transform);
        }

        #endregion

        #region Private Methods

        private void SelectTarget()
        {
            switch (attackOrder)
            {
                case AttackOrder.First:
                    Cast(enteredEnemies.First());
                    break;
                case AttackOrder.Last:
                    Cast(enteredEnemies.Last());
                    break;
                case AttackOrder.Closest:
                    var closest = enteredEnemies.OrderBy(enemy => Vector3.Distance(enemy.position, transform.position)).ToArray();
                    Cast(closest.First());
                    break;
                case AttackOrder.Strongest:
                    var strongest = enteredEnemies.OrderBy(enemy => enemy.GetComponent<Enemy>().EnemyCombatScore).ToArray();
                    Cast(strongest.Last());
                    break;
                case AttackOrder.Weakest:
                    var weakest = enteredEnemies.OrderBy(enemy => enemy.GetComponent<Enemy>().EnemyCombatScore).ToArray();
                    Cast(weakest.First());
                    break;
                case AttackOrder.HighestHealth:
                    var highestHealth = enteredEnemies.OrderBy(enemy => enemy.GetComponent<Enemy>().EnemyHealth).ToArray();
                    Cast(highestHealth.Last());
                    break;
                case AttackOrder.LowestHealth:
                    var lowestHealth = enteredEnemies.OrderBy(enemy => enemy.GetComponent<Enemy>().EnemyHealth).ToArray();
                    Cast(lowestHealth.First());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool TriggerReturns(Component other)
        {
            return Ability.AbilityAbilityObj.AbilityType == AbilityType.Damage && other.CompareTag("Enemy");
        }

        /// <summary>
        /// Creates an Ability dont look into it :)
        /// </summary>
        /// <param name="unitCard"></param>
        private void CreateAbility(BaseCard unitCard)
        {
            var type = unitCard.abilityDataBase.Abilities[0].GetType();
            if (type.IsSubclassOf(typeof(DamageAbilityObj)))
            {
                var abilityObj = (DamageAbilityObj) unitCard.abilityDataBase.Abilities[0];
                var damageAbility = abilityObj.CreateInstance<DamageAbility>();
                Ability = damageAbility;
                sphereCollider.radius = damageAbility.AttackRange;
            }
            else if (type.IsSubclassOf(typeof(UtilityAbilityObj)))
            {
                var abilityObj = (UtilityAbilityObj) unitCard.abilityDataBase.Abilities[0];
                var utilityAbility = abilityObj.CreateInstance<UtilityAbility>();
                Ability = utilityAbility;
                sphereCollider.radius = utilityAbility.EffectRange;
            }
            client.Visitors.Add(Ability);
        }

        private void Cast(Transform target)
        {
            if (Ability.TimeLeft > 0) 
                return;
            if (Ability.AbilityAbilityObj.AbilityType == AbilityType.Damage)
                FixRotation(target);
            
            Ability.Cast(spawnPos, target, Caster.Unit);
        }

        private void FixRotation(Transform target)
        {
            var rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up).eulerAngles;
            rotation.z = 0;
            rotation.x = 0;
            transform.rotation = Quaternion.Euler(rotation);
        }

        #endregion

        #region Public Methods

        public void Initialize(UnitCard unitCard)
        {
            //Base Implement
            faction = unitCard.Faction;
            unitClass = unitCard.Class;
            CreateAbility(unitCard);
            SkillTree = unitCard.SkillTreeObj.CreateInstance(client);
        }

        #endregion

        #region Visitor Pattern

        public void Visit(Node node)
        {
            node.Accept(this);
        }

        #endregion
    }
}