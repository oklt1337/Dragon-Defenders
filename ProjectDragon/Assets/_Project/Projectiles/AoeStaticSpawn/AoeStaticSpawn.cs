using _Project.AI.Enemies.Scripts;
using _Project.Enemies.Scripts;
using _Project.Scripts.Gameplay.Enemies;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Projectiles
{
    public class AoeStaticSpawn : MonoBehaviour
    {
        //objects and  should know their own size and duration because of their animations
        //but for now this can help for testing
        [SerializeField]
        private float lifeTime = 1.2f;
        [SerializeField]
        private float damage = 20;

        public float LifeTime
        {
            get => lifeTime;
            set => lifeTime = value;
        }
        

        public float Damage
        {
            get => damage;
            set => damage = value;
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

        private void OnTriggerEnter(Collider other)
        {
            Enemy hit = other.GetComponent<Enemy>();
            if (hit != null)
            {
                hit.TakeDamage(damage);
            }
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
