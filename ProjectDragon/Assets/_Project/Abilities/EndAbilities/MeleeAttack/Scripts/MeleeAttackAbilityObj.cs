using Abilities.Ability.Scripts;
using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.EndAbilities.MeleeAttack.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/Abilities/DamageAbilities/MeleeAttack", fileName = "MeleeAttack")]
    public class MeleeAttackAbilityObj : DamageAbilityObj
    {
        public static void Cast(Transform spawnPoint, Caster caster, float abilityDamage, float abilityStun)
        {
            spawnPoint.GetComponent<MeleeProjectile>().Init(caster, abilityDamage, abilityStun);
        }

        public override T CreateInstance<T>()
        {
            return new MeleeAttackAbility(this) as T;
        }
    }

    public class MeleeAttackAbility : DamageAbility
    {
        public float StunTime { get; set; }
        public MeleeAttackAbility(DamageAbilityObj abilityObj) : base(abilityObj)
        {
            StunTime = 0;
        }
        
        public override void Cast(Transform spawnPoint, Transform target, Caster caster)
        {
            if (TimeLeft > 0) 
                return;
            StartCooldown = true;
            Casted?.Invoke(target);
            
            MeleeAttackAbilityObj.Cast(spawnPoint, caster, Damage, StunTime);
        }
    }
}