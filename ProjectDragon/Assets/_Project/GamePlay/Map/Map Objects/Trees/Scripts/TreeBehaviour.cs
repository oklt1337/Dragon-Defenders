using _Project.GamePlay.Map.Map_Objects.Base_Object.Scripts;
using UnityEngine;

namespace _Project.GamePlay.Map.Map_Objects.Trees.Scripts
{
    public class TreeBehaviour : BaseMapObject
    {
        [SerializeField] private GameObject interior;
        [SerializeField] private float health;

        #region Unity Methods

        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Ability"))
                return;

            //var projectile = other.gameObject.GetComponent<Projectile>();
            //TakeDamage(projectile);
        }

        private void OnDestroy()
        {
            if (interior == null)
                return;
        }

        #endregion

        /*private void TakeDamage(Projectile projectile)
        {
            health -= projectile.Damage;
            
            if(health <= 0)
                Destroy(gameObject);
        }*/
    }
}