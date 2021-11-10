using System;
using Abilities.Ability.Scripts;
using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.VisitorPattern.Scripts;
using Deck_Cards.Cards.BaseCards.Scripts;
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
                //ability.Cast(spawnPos, null);
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
            CreateAbility(unitCard);
            skillTree = unitCard.SkillTreeObj.CreateInstance(client);
        }

        private void CreateAbility(BaseCard unitCard)
        {
            var type = unitCard.abilityDataBase.Abilities[0].GetType();
            if (type == typeof(SingleShotAbilityObj))
            {
                var abilityObj = (SingleShotAbilityObj) unitCard.abilityDataBase.Abilities[0];
                var damageAbility = abilityObj.CreateInstance();
                ability = damageAbility;
                sphereCollider.radius = damageAbility.AttackRange;
            }
            else if (type == typeof(UtilityAbility))
            {
                var utilityAbilityObj = (UtilityAbilityObj) unitCard.abilityDataBase.Abilities[0];
                var utilityAbility = utilityAbilityObj.CreateInstance();
                ability = utilityAbility;
                sphereCollider.radius = utilityAbility.EffectRange;
            }
            client.Visitors.Add(ability);
        }

        private void Cast(Transform target)
        {
            var type = ability.GetType();
            if (type == typeof(SingleShotAbility))
            {
                ((SingleShotAbility) ability).Cast(spawnPos, target);
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