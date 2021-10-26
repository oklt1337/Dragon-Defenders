using _Project.AI.Enemies.Scripts;
using _Project.Projectiles.BaseProjectile;
using UnityEngine;

namespace _Project.Projectiles.AoeStaticSpawn
{
    public class AoeStaticSpawn : Projectile
    {
        //objects and  should know their own size and duration because of their animations
        //but for now this can help for testing
        [SerializeField]
        private float lifeTime = 1.2f;
        public float LifeTime
        {
            get => lifeTime;
            set => lifeTime = value;
        }
        

        private void LifeTimeCountDown()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                DeSpawn();
            }
        }
        private void Start()
        {
            
        }

        protected override void Move()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            Enemy hit = other.GetComponent<Enemy>();
            if (hit == null)
                return;
            
            hit.TakeDamage(damage);
            
            if (knockBack <= 0) return;
            
            hit.GetComponent<Rigidbody>().AddForce(transform.forward * knockBack);
        }
        
        private void DeSpawn()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            LifeTimeCountDown();
        }
    }
}
