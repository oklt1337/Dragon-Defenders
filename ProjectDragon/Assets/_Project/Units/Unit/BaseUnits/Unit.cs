using System;
using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using Deck_Cards.Cards.UnitCard.Scripts;
using Faction;
using Sirenix.OdinInspector;
using SkillSystem.SkillTree.Scripts;
using UnityEngine;

namespace Units.Unit.BaseUnits
{
    [RequireComponent(typeof(SphereCollider))]
    public class Unit : MonoBehaviour, IVisitor
    {
        #region Public Const Field

        public const string BasePath = "Cards/UnitCards/";

        #endregion

        #region SerializeFields

        [SerializeField] private Animator animator;
        [SerializeField] private Transform spawnPos;
        [SerializeField] private SphereCollider sphereCollider;

        #endregion

        #region Private Fields

        [SerializeField] private UnitCard card;
        private ClassAndFaction.Faction faction;
        private ClassAndFaction.Class unitClass;
        private SkillTree skillTree;
        private Ability ability;
        private Client client;

        #endregion

        #region Public Propeties

        public Animator Animator
        {
            get => animator;
            set => animator = value;
        }

        public UnitCard Card => card;

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

        public SkillTree SkillTree
        {
            get => skillTree;
            set => skillTree = value;
        }

        public Ability Ability
        {
            get => ability;
            set => ability = value;
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            client = new Client();

            if (sphereCollider == null)
            {
                sphereCollider = GetComponent<SphereCollider>();
            }
        }

        
        private void OnTriggerStay(Collider other)
        {
            if (ability.AbilityObj.AbilityType == AbilityType.Damage)
            {
                if (!other.CompareTag("Enemy"))
                    return;

                Cast(other.transform);
            }
            else
            {
                // Handle Buff
            }
        }

        private void Update()
        {
            if (ability == null)
                return;

            if (!ability.Casted && !(ability.TimeLeft > 0)) 
                return;
            ability.Tick(Time.deltaTime);
        }

        #endregion

        #region Private Methods

        [Button]
        protected virtual void Initialize(UnitCard unitCard)
        {
            //Base Implement
            card = unitCard;
            faction = unitCard.Faction;
            unitClass = unitCard.Class;

            switch (unitCard.abilityDataBase.Abilities[0].AbilityType)
            {
                case AbilityType.Damage:
                    var damageAbilityObj = (DamageAbilityObj) unitCard.abilityDataBase.Abilities[0];
                    var damageAbility = damageAbilityObj.CreateInstance();
                    ability = damageAbility;
                    sphereCollider.radius = damageAbility.AttackRange;
                    client.Visitors.Add(damageAbility);
                    break;
                case AbilityType.Utility:
                    var utilityAbilityObj = (UtilityAbilityObj) unitCard.abilityDataBase.Abilities[0];
                    var utilityAbility = utilityAbilityObj.CreateInstance();
                    ability = utilityAbility;

                    sphereCollider.radius = utilityAbility.EffectRange;
                    client.Visitors.Add(utilityAbility);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            skillTree = unitCard.SkillTreeObj.CreateInstance(client);
        }

        private void Cast(Transform target)
        {
            switch (ability.AbilityObj.AbilityType)
            {
                case AbilityType.Damage:
                    var damageAbility = (DamageAbility) ability;
                    damageAbility.Cast(spawnPos, target);
                    break;
                case AbilityType.Utility:
                    var utilityAbility = (UtilityAbility) ability;
                    utilityAbility.Cast(transform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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