using Abilities.Ability.Scripts;
using Abilities.EndAbilities.AOE_Area.Scripts;
using Abilities.EndAbilities.HomingShot.Scripts;
using Abilities.EndAbilities.IncreaseDamageForSetTime.Scripts;
using Abilities.EndAbilities.MeleeAttack.Scripts;
using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using Abilities.VisitorPattern.Scripts;
using Deck_Cards.Cards.BaseCards.Scripts;
using Deck_Cards.Cards.UnitCard.Scripts;
using Faction;
using SkillSystem.SkillTree.Scripts;
using UnityEngine;

namespace Units.Unit.BaseUnits
{
    [RequireComponent(typeof(SphereCollider))]
    public sealed class Unit : MonoBehaviour, IVisitor
    {
        #region SerializeFields

        [SerializeField] private Animator animator;
        [SerializeField] private Transform spawnPos;
        [SerializeField] private SphereCollider sphereCollider;

        #endregion

        #region Private Fields

        private UnitCard card;
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

            if (!ability.StartCooldown && !(ability.TimeLeft > 0)) 
                return;
            ability.Tick(Time.deltaTime);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates an Ability dont look into it :)
        /// </summary>
        /// <param name="unitCard"></param>
        private void CreateAbility(BaseCard unitCard)
        {
            //sry Patrick ist obsolete
            //spaeter dann i wann richtiges ability system das das ganze fixet :)
            var type = unitCard.abilityDataBase.Abilities[0].GetType();
            if (type == typeof(SingleShotAbilityObj))
            {
                var abilityObj = (SingleShotAbilityObj) unitCard.abilityDataBase.Abilities[0];
                var damageAbility = abilityObj.CreateInstance<SingleShotAbility>();
                ability = damageAbility;
                sphereCollider.radius = damageAbility.AttackRange;
            }
            else if (type == typeof(MeleeAttackAbilityObj))
            {
                var abilityObj = (MeleeAttackAbilityObj) unitCard.abilityDataBase.Abilities[0];
                var damageAbility = abilityObj.CreateInstance<MeleeAttackAbility>();
                ability = damageAbility;
                sphereCollider.radius = damageAbility.AttackRange;
            }
            else if (type == typeof(HomingShotAbilityObj))
            {
                var abilityObj = (HomingShotAbilityObj) unitCard.abilityDataBase.Abilities[0];
                var damageAbility = abilityObj.CreateInstance<HomingShotAbility>();
                ability = damageAbility;
                sphereCollider.radius = damageAbility.AttackRange;
            }
            else if (type == typeof(AoeAreaAbilityObj))
            {
                var abilityObj = (AoeAreaAbilityObj) unitCard.abilityDataBase.Abilities[0];
                var damageAbility = abilityObj.CreateInstance<AoeAreaAbility>();
                ability = damageAbility;
                sphereCollider.radius = damageAbility.AttackRange;
            }
            else if (type == typeof(IncreaseDamageForSetTimeAbilityObj))
            {
                var abilityObj = (IncreaseDamageForSetTimeAbilityObj) unitCard.abilityDataBase.Abilities[0];
                var utilityAbility = abilityObj.CreateInstance<IncreaseDamageForSetTimeAbility>();
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
                ((SingleShotAbility) ability).Cast(spawnPos, target, Caster.Unit);
            }
            else if (type == typeof(MeleeAttackAbility))
            {
                ((MeleeAttackAbility) ability).Cast(spawnPos, target, Caster.Unit);
            }
            else if (type == typeof(HomingShotAbility))
            {
                ((HomingShotAbility) ability).Cast(spawnPos, target, Caster.Unit);
            }
            else if (type == typeof(AoeAreaAbility))
            {
                ((AoeAreaAbility) ability).Cast(spawnPos, target, Caster.Unit);
            }
            else if (type == typeof(IncreaseDamageForSetTimeAbility))
            {
                ((IncreaseDamageForSetTimeAbility) ability).Cast(spawnPos, target, Caster.Unit);
            }
        }

        #endregion

        #region Visitor Pattern

        public void Visit(Node node)
        {
            node.Accept(this);
        }
        
        public void Initialize(UnitCard unitCard)
        {
            //Base Implement
            card = unitCard;
            faction = unitCard.Faction;
            unitClass = unitCard.Class;
            CreateAbility(unitCard);
            skillTree = unitCard.SkillTreeObj.CreateInstance(client);
        }

        #endregion
    }
}