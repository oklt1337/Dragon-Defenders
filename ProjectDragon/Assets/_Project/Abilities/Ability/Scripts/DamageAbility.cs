using Abilities.Projectiles;
using Abilities.Projectiles.Scripts;
using UnityEngine;

namespace Abilities.Ability.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Abilities/DamageAbility", fileName = "DamageAbility")]
    public abstract class DamageAbilityObj : AbilityObj
    {
        [SerializeField] private float damage;
        [SerializeField] private float attackRange;
        
        [SerializeField] private GameObject prefabProjectile;
        [SerializeField] private DamageProjectile damageProjectile;

        public float Damage => damage;
        public float AttackRange => attackRange;
        
        public DamageAbility CreateInstance()
        {
            return new DamageAbility(this);
        }

        public void Cast(Transform spawnPoint, Transform target, float abilityDamage)
        {
            //Spawn projectile
            damageProjectile = Instantiate(prefabProjectile, spawnPoint.position, Quaternion.identity, spawnPoint).GetComponent<DamageProjectile>();
            damageProjectile.Init(target != null ? target : null, abilityDamage);
        }
    }
    
    public class DamageAbility : Ability
    {
        public float Damage { get; set; }
        public float AttackRange { get; set; }

        public DamageAbility(DamageAbilityObj abilityObj) : base(abilityObj)
        {
            Damage = abilityObj.Damage;
            AttackRange = abilityObj.AttackRange;
        }

        public void Cast(Transform spawnPoint, Transform target)
        {
            var damageAbilityObj = (DamageAbilityObj) AbilityObj;
            damageAbilityObj.Cast(spawnPoint, target, Damage);
        }
    }
}
