using AI.Enemies.Base_Enemy;
using UnityEngine;

namespace AI.Enemies.Scripts
{
    public abstract class Mover : Enemy
    {
        [SerializeField] private float speedUpValue;

        private void SpeedUp()
        {
            speed = speedUpValue;
        }

        protected override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            
            if(health*2 <= maxHealth)
                SpeedUp();
        }
    }
}
