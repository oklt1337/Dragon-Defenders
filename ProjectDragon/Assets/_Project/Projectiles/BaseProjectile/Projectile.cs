using UnityEngine;

namespace _Project.Projectiles.BaseProjectile
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float speed = 10;
        [SerializeField] protected float damage = 100;
        [SerializeField] protected float knockBack;

        public float KnockBack
        {
            get => knockBack;
            set => knockBack = value;
        }

        public float Damage
        {
            get => damage;
            set => damage = value;
        }
    
        public float Speed
        {
            get => speed;
            set => speed = value;
        }
        protected abstract void Move();
    }
}
