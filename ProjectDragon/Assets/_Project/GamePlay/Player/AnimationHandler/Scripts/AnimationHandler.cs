using UnityEngine;

namespace GamePlay.Player.AnimationHandler.Scripts
{
    public class AnimationHandler : MonoBehaviour
    {
        #region SerializeFields

        #endregion

        #region Private Fields

        private Animator _animator;

        #endregion

        #region Protected Fields

        #endregion

        #region Public Fields

        #endregion

        #region Public Properties
        
        public Animator Animator
        {
            get => _animator;
            set => _animator = value;
        }

        #endregion

        #region Events

        #endregion

        #region Unity Methods

        #endregion

        #region Private Methods

        private void ChangeAnimation(AnimationClip animationClip)
        {
            
        }

        #endregion

        #region Protected Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
