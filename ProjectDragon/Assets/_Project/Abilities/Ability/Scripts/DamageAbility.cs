using UnityEngine;

namespace Abilities.Ability.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Abilities/DamageAbility", fileName = "DamageAbility")]
    public abstract class DamageAbilityObj : AbilityObj
    {
        public DamageAbility CreateInstance()
        {
            return new DamageAbility(this);
        }

        public override void Cast(Transform spawnPoint, Transform target)
        {
            //Spawn projectile or Cast Buff
        }
    }
    
    public class DamageAbility : Ability
    {
        public float Damage { get; set; }
        public float AttackRange { get; set; }
        
        public DamageAbility(AbilityObj abilityObj) : base(abilityObj) { }
    }
}
