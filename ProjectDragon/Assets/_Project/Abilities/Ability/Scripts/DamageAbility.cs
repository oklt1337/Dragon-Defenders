using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.Ability.Scripts
{
    public enum DamageType
    {
        Piercing,
        Burn
    }
    
    public abstract class DamageAbilityObj : AbilityObj
    {
        [SerializeField] private float damage;
        [SerializeField] private float attackRange;
        [SerializeField] private GameObject prefabProjectile;

        public float Damage => damage;
        public float AttackRange => attackRange;
        protected GameObject PrefabProjectile => prefabProjectile;
    }

    public abstract class DamageAbility : Ability
    {
        public float Damage { get; set; }
        public float AttackRange { get; set; }
        public DamageType DamageType { get; set; }

        public DamageAbility(DamageAbilityObj abilityAbilityObj) : base(abilityAbilityObj)
        {
            Damage = abilityAbilityObj.Damage;
            AttackRange = abilityAbilityObj.AttackRange;
        }


        public abstract void Cast(Transform spawnPoint, Transform target, Caster caster);
    }
}