using System;
using _Project.AI.Enemies.Scripts;
using _Project.Enemies.Scripts;
using _Project.Scripts.Gameplay.Enemies;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Project.Scripts.Gameplay.Projectiles
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
            if (hit != null)
            {
                hit.TakeDamage(damage); 
                DeSpawn();
            }
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
