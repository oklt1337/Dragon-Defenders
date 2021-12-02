using UnityEngine;

namespace AbilitySystem.Entity.Scripts
{
    public abstract class Entity : MonoBehaviour
    {
        public Handler.Scripts.Handler Handler { get; protected set; }
    }
}