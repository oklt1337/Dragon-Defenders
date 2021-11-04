using UnityEngine;

namespace Abilities.Projectiles.Scripts
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected Rigidbody myRigidbody;
        [SerializeField] protected Collider myCollider;
    }
}