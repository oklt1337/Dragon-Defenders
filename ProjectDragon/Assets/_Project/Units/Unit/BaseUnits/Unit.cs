using Abilities.Ability.Scripts;
using Abilities.EndAbilities.AOE_Area.Scripts;
using Abilities.EndAbilities.HomingShot.Scripts;
using Abilities.EndAbilities.IncreaseDamageForSetTime.Scripts;
using Abilities.EndAbilities.MeleeAttack.Scripts;
using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.EndAbilities.SingleShotSetRange.Scripts;
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

            if (sphereCollider == null)
            {
                sphereCollider = GetComponent<SphereCollider>();
            }
        }

        
        private void OnTriggerStay(Collider other)
        {
            if (Ability.AbilityAbilityObj.AbilityType == AbilityType.Damage)
            {
                if (!other.CompareTag("Enemy"))
                    return;
                if (Ability.TimeLeft > 0) 
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
            if (Ability == null)
                return;
            if (!Ability.StartCooldown && !(Ability.TimeLeft > 0)) 
                return;
            Ability.Tick(Time.deltaTime);
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
            if (type == typeof(SingleShotAbilityObj) 
                || type == typeof(MeleeAttackAbilityObj)
                || type == typeof(HomingShotAbilityObj)
                || type == typeof(AoeAreaAbilityObj)
                || type == typeof(SingleShotSetRangeAbilityObj))
            {
                var abilityObj = (DamageAbilityObj) unitCard.abilityDataBase.Abilities[0];
                var damageAbility = abilityObj.CreateInstance<DamageAbility>();
                Ability = damageAbility;
                sphereCollider.radius = damageAbility.AttackRange;
            }
            else if (type == typeof(IncreaseDamageForSetTimeAbilityObj))
            {
                var abilityObj = (IncreaseDamageForSetTimeAbilityObj) unitCard.abilityDataBase.Abilities[0];
                var utilityAbility = abilityObj.CreateInstance<UtilityAbility>();
                Ability = utilityAbility;
                sphereCollider.radius = utilityAbility.EffectRange;
            }
            client.Visitors.Add(Ability);
        }

        private void Cast(Transform target)
        {
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

        #region Visitor Pattern

        public void Visit(Node node)
        {
            node.Accept(this);
        }
        
        public void Initialize(UnitCard unitCard)
        {
            //Base Implement
            faction = unitCard.Faction;
            unitClass = unitCard.Class;
            CreateAbility(unitCard);
            SkillTree = unitCard.SkillTreeObj.CreateInstance(client);
        }

        #endregion
    }
}