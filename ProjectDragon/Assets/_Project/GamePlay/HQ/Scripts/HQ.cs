using System;
using UnityEngine;

namespace _Project.GamePlay.HQ.Scripts
{
    public class HQ : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private GameObject hqObj;
        [SerializeField] private float health;

        #endregion

        #region Private Fields

        #endregion

        #region Protected Fields

        #endregion

        #region Public Fields

        #endregion

        #region Public Properties

        #endregion

        #region Unity Methods

        public event Action OnDeath;
        
        #endregion

        #region Private Methods

        #endregion

        #region Protected Methods

        #endregion

        #region Public Methods

        public void TakeDamage(int damage)
        {
            health -= damage;

            if (!(health <= 0)) 
                return;
            
            health = 0;
            OnDeath?.Invoke();
        }
        
        #endregion
    }
}
