using GamePlay.Map.Map_Objects.Base_Object.Scripts;
using UnityEngine;

namespace GamePlay.Map.Map_Objects.Trees.Scripts
{
    public class DestroyableMapObject : BaseMapObject
    {
        [SerializeField] private GameObject interior;
        [SerializeField] private float health;

        #region Unity Methods

        private void OnDestroy()
        {
            if (interior == null)
                return;
        }

        #endregion

        private void TakeDamage(float damage)
        {
            health -= damage;
            
            if(health <= 0)
                Destroy(gameObject);
        }
    }
}