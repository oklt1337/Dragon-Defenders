using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using Photon.Pun;
using UnityEngine;

namespace Abilities.Ability.Scripts
{
    public abstract class DamageAbilityObj : AbilityObj
    {
        [SerializeField] private float damage;
        [SerializeField] private float attackRange;
        [SerializeField] private GameObject prefabProjectile;

        public float Damage => damage;
        public float AttackRange => attackRange;
        public GameObject PrefabProjectile => prefabProjectile;
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

        public override void Cast(Transform spawnPoint, Transform target, Caster caster)
        {
            if (TimeLeft > 0) 
                return;
            StartCooldown = true;
            Casted?.Invoke();
        }
    }
}