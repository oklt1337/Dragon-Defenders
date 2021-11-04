using AI.Enemies.Base_Enemy;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
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
            if (other.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Enemy>();
                enemy.TakeDamage(Damage);
            }
            else if(other.CompareTag("Player"))
            {
                var commander = other.GetComponent<Commander>();
                commander.TakeDamage(Damage);
            }
            else if (other.CompareTag("Breakable"))
            {
                /*var baseMapObject = other.GetComponent<Tree>();
                baseMapObject.TakeDamage(Damage);*/
            }
        }
    }
}