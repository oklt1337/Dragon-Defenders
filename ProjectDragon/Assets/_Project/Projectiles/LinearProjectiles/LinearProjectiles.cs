using _Project.AI.Enemies.Scripts;
using _Project.Projectiles;
using _Project.Projectiles.BaseProjectile;
using UnityEngine;

namespace _Project.Projectiles.LinearProjectiles
{
    public class LinearProjectiles : Projectile
    { 
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float lifetime = 2;
        
        
        
        protected override void Move()
        {
            rb.velocity = transform.forward * speed;
        }

        public void SetLifeTime(float maxDistance)
        {
            lifetime = maxDistance/ speed;
        }

        private void LifeTimeCountDown()
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                DeSpawn();
            }
        }
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Enemy hit = other.GetComponent<Enemy>();
            if (hit == null)
                return;
            hit.TakeDamage(damage); 
                
            if (knockBack > 0)
                hit.GetComponent<Rigidbody>().AddForce(transform.forward * knockBack);

            DeSpawn();
            
        }

        private void DeSpawn()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            Move();
            LifeTimeCountDown();
        }
    }
}
