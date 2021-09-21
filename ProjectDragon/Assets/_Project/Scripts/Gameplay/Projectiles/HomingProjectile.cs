using System;
using _Project.Scripts.Gameplay.Enemies;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Projectiles
{
    public class HomingProjectile : Projectile
    {  
        [SerializeField] private Transform target;

        private Vector3 _spawnPosition;
        private float _lerpValue = 0;
        
        public Transform Target
        {
            get => target;
            set => target = value;
        }
        

        protected void Start()
        {
            _spawnPosition = transform.position;
        }

        protected void Update()
        {
            Move();
            
            CheckPosition();
        }

        protected override void Move()
        {
            if(!target) DeSpawn();
            
            transform.rotation = Quaternion.LookRotation(target.transform.position);
            transform.position = Vector3.Lerp(_spawnPosition,
                target.position,
                _lerpValue +(speed * Time.deltaTime));
            _lerpValue += speed * Time.deltaTime;
        }

        protected void CheckPosition()
        {
            if (transform.position == target.transform.position)
            {
                DeSpawn();
            }
        }

        protected void DeSpawn()
        {
            Destroy(gameObject);
        }
        
        public void OnTriggerEnter(Collider other)
        {
            Enemy hit = other.GetComponent<Enemy>();
            if (hit != null)
            {
                hit.TakeDamage(damage);
                DeSpawn();
            }
        }
    }
}