using _Project.AI.Enemies.Scripts;
using _Project.Projectiles.BaseProjectile;
using UnityEngine;

namespace _Project.Projectiles.HominProjectile
{
    public class HomingProjectile : Projectile
    {  
        [SerializeField] private Transform target;

        private Vector3 spawnPosition;
        private float lerpValue = 0;
        
        public Transform Target
        {
            get => target;
            set => target = value;
        }
        

        protected void Start()
        {
            spawnPosition = transform.position;
            lerpValue = 0;
        }

        protected void Update()
        {
            Move();
            
            CheckPosition();
        }

        protected override void Move()
        {
            if(!target|| !target.gameObject.activeSelf ) DeSpawn();
            
            transform.rotation = Quaternion.LookRotation(target.transform.position);
            transform.position = Vector3.Lerp(spawnPosition,
                target.position,
                lerpValue +(speed * Time.deltaTime));
            lerpValue += speed * Time.deltaTime;
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
            if (hit == null)
                return;
            
            hit.TakeDamage(damage);
            
            if (knockBack > 0)
                hit.GetComponent<Rigidbody>().AddForce(transform.forward * knockBack);
            
            DeSpawn();
        }
    }
}