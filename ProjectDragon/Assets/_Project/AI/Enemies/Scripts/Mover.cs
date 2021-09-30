using _Project.AI.Enemies.Scripts;
using UnityEngine;

namespace _Project.Enemies.Scripts
{
    public abstract class Mover : Enemy
    {
        [SerializeField] private float speedUpValue;

        private void SpeedUp()
        {
            speed = speedUpValue;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            
            if(health*2 <= maxHealth)
                SpeedUp();
        }
    }
}
