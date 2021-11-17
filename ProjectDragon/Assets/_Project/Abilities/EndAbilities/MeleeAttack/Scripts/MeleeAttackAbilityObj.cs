using Abilities.Ability.Scripts;
using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using UnityEngine;

namespace Abilities.EndAbilities.MeleeAttack.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Abilities/MeleeAttack", fileName = "MeleeAttack")]
    public class MeleeAttackAbilityObj : DamageAbilityObj
    {
        [SerializeField] private float duration;
        public float Duration => duration;
        
        public void Cast(Transform spawnPoint, Caster caster, float abilityDamage, float abilityDuration)
        {
            //Spawn projectile
            var projectile = Instantiate(PrefabProjectile, spawnPoint.position, Quaternion.identity, spawnPoint).GetComponent<MeleeProjectile>();
            projectile.Init(caster, abilityDamage, abilityDuration);
        }


        public override T CreateInstance<T>()
        {
            return new MeleeAttackAbility(this) as T;
        }
    }

    public class MeleeAttackAbility : DamageAbility
    {
        public float Duration { get; set; }
        
        public MeleeAttackAbility(MeleeAttackAbilityObj abilityObj) : base(abilityObj)
        {
            Duration = abilityObj.Duration;
        }
        
        public override void Cast(Transform spawnPoint, Transform target, Caster caster)
        {
            if (TimeLeft > 0) 
                return;
            StartCooldown = true;
            Casted?.Invoke();

            var aoeAreaAbility = (MeleeAttackAbilityObj) AbilityObj;
            aoeAreaAbility.Cast(spawnPoint, caster, Damage, Duration);
        }
    }
}