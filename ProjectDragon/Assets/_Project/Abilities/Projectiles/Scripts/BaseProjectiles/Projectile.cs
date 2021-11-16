using UnityEngine;

namespace Abilities.Projectiles.Scripts.BaseProjectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected Rigidbody myRigidbody;
        [SerializeField] protected Collider myCollider;
    }
}