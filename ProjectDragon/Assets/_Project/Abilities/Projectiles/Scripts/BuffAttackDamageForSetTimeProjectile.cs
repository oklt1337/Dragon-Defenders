using Abilities.Effects.IncreaseDamageEffect.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public class BuffAttackDamageForSetTimeProjectile : UtilityProjectile
    {
        private float buffDuration;
        private float value;
        
        public void Init(float effectRange, float duration, float buffValue)
        {
            ((SphereCollider) myCollider).radius = effectRange;
            Duration = duration;
            buffDuration = duration;
            value = buffValue;
        }

        protected void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Unit")) 
                return;
            if (other.GetComponent<IncreaseDamageEffect>() != null) 
                return;
            var effect = other.AddComponent<IncreaseDamageEffect>();
            effect.Init(buffDuration, value);
        }
    }
}