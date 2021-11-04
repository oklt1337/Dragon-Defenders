using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public class UtilityProjectile : Projectile
    {
        public void Init(float effectRange)
        {
           ((SphereCollider) myCollider).radius = effectRange;
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                
            }
            else if (other.CompareTag("Unit"))
            {
                
            }
        }
    }
}