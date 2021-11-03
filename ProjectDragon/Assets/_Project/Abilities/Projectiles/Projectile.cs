using AI.Enemies.Base_Enemy;
using UnityEngine;

namespace Abilities.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody myRigidbody;

        public float Damage { get; set; }

        public void Init(Transform target)
        {
            myRigidbody.MovePosition(target == null ? Vector3.forward : target.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Enemy>();
            //enemy.TakeDamage(Damage);
        }
    }
}