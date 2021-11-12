using UnityEngine;

namespace GamePlay.Player.AnimationHandler.Scripts
{
    public class AnimationHandler : MonoBehaviour
    {
        #region Private Fields

        private Animator animator;

        #endregion

        #region Public Properties
        
        public Animator Animator
        {
            get => animator;
            set => animator = value;
        }

        #endregion

        #region Private Methods

        private void ChangeAnimation(AnimationClip animationClip)
        {
            
        }

        #endregion
    }
}
