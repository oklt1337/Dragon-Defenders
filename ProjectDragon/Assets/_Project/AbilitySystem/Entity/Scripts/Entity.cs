using UnityEngine;

namespace AbilitySystem.Entity.Scripts
{
    public abstract class Entity : MonoBehaviour
    {
        public Handler.Scripts.Handler Handler { get; protected set; }

        private void Start()
        {
            Handler = new Handler.Scripts.Handler(this);
        }

        public virtual void Update()
        {
            if (Handler.HasEntries)
                Handler.ProcessEntries();
        }
    }
}