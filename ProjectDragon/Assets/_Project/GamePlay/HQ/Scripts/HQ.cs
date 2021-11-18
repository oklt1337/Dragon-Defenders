using System;
using UnityEngine;

namespace GamePlay.HQ.Scripts
{
    public class HQ : MonoBehaviour
    {
        #region SerializeFields
        
        [SerializeField] private float health;

        #endregion

        #region Events

        public event Action<float> OnHqHealthChanged; 

        #endregion

        #region Unity Methods

        public event Action OnDeath;
        
        #endregion

        #region Public Methods

        public void TakeDamage(int damage)
        {
            health -= damage;
            OnHqHealthChanged?.Invoke(health);

            if (!(health <= 0)) 
                return;
            
            health = 0;
            OnDeath?.Invoke();
        }
        
        #endregion
    }
}
