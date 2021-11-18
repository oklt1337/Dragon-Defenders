using UnityEngine;

namespace Abilities.Projectiles.Scripts.BaseProjectiles
{
    public class UtilityProjectile : Projectile
    {
        protected Transform Owner { get; set; }
        protected float Duration { get; set; }

        protected virtual void Update()
        {
            Duration -= Time.deltaTime;
            if (Duration <= 0)
                Destroy(gameObject);
        }
    }
}