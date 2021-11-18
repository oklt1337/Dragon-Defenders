using System;
using Abilities.Ability.Scripts;
using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using Photon.Pun;
using UnityEngine;

namespace Abilities.EndAbilities.DecreaseCooldownOfDamageAbility.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Abilities/DecreaseCooldownOfDamage", fileName = "DecreaseCooldownOfDamage")]
    public class DecreaseCooldownOfDamageAbilityObj : UtilityAbilityObj
    {
        [SerializeField] private float decreaseCooldownValueInPercentage;
        public float DecreaseCooldownValueInPercentage => decreaseCooldownValueInPercentage;

        public void Cast(Transform spawnPoint, float abilityEffectRange, float abilityDecreaseCooldownValueInPercentage, ref Action<Transform> action)
        {
            //Spawn projectile
            if (spawnPoint.GetComponent<ReduceCooldownOfDamageAbilityProjectile>() != null)
                return;
            
            var projectile = Instantiate(prefabProjectile, spawnPoint.position, Quaternion.identity, spawnPoint)
                .GetComponent<ReduceCooldownOfDamageAbilityProjectile>();
            projectile.Init(spawnPoint, abilityEffectRange, abilityDecreaseCooldownValueInPercentage);
            var myAction = action;
            projectile.OnTargetChanged += transform => myAction?.Invoke(transform);
        }

        public override T CreateInstance<T>()
        {
            return new DecreaseCooldownOfDamageAbility(this) as T;
        }
    }

    public class DecreaseCooldownOfDamageAbility : UtilityAbility
    {
        public float DecreaseCooldownValueInPercentage { get; set; }
        public event Action<Transform> OnTargetChanged;

        public DecreaseCooldownOfDamageAbility(DecreaseCooldownOfDamageAbilityObj abilityObj) : base(abilityObj)
        {
            DecreaseCooldownValueInPercentage = abilityObj.DecreaseCooldownValueInPercentage;
        }

        public override void Cast(Transform spawnPoint, Transform target, Caster caster)
        {
            if (TimeLeft > 0)
                return;
            StartCooldown = true;
            Casted?.Invoke();

            var abilityObj = (DecreaseCooldownOfDamageAbilityObj) AbilityAbilityObj;
            abilityObj.Cast(spawnPoint, EffectRange, DecreaseCooldownValueInPercentage, ref OnTargetChanged);
        }
    }
}