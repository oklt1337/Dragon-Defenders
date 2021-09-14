using UnityEngine;
using static _Project.Scripts.Gameplay.GameManager;

namespace _Project.Scripts.Gameplay.Enemies
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
