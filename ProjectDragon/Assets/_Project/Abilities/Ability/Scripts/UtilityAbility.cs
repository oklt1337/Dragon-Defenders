using UnityEngine;

namespace Abilities.Ability.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Abilities/UtilityAbility", fileName = "UtilityAbility")]
    public abstract class UtilityAbilityObj : AbilityObj
    {
        public UtilityAbility CreateInstance()
        {
            return new UtilityAbility(this);
        }

        public override void Cast(Transform spawnPoint, Transform target)
        {
            //Spawn projectile or Cast Buff
        }
    }
    
    public class UtilityAbility : Ability
    {
        public float EffectRange { get; set; }
        
        public UtilityAbility(AbilityObj abilityObj) : base(abilityObj) { }
    }
}
