using System;
using GamePlay.Player.PlayerModel.Scripts;
using UnityEngine;

namespace GamePlay.Player.AnimationHandler.Scripts
{
    public class AnimationHandler : MonoBehaviour
    {
        #region Private Fields
        
        private Animator animator;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int DoesBaseAttack = Animator.StringToHash("DoesBaseAttack");
        private static readonly int IsCasting1 = Animator.StringToHash("IsCasting1");
        private static readonly int IsCasting2 = Animator.StringToHash("IsCasting2");
        private static readonly int IsCasting3 = Animator.StringToHash("IsCasting3");

        #endregion

        #region Private Methods

        private void ChangeAnimation(State state)
        {
            switch (state)
            {
                case State.Idle:
                    animator.SetBool(IsMoving, false);
                    animator.SetBool(DoesBaseAttack, false);
                    animator.SetBool(IsCasting1, false);
                    animator.SetBool(IsCasting2, false);
                    animator.SetBool(IsCasting3, false);
                    break;
                case State.Move:
                    animator.SetBool(IsMoving, true);
                    animator.SetBool(DoesBaseAttack, false);
                    animator.SetBool(IsCasting1, false);
                    animator.SetBool(IsCasting2, false);
                    animator.SetBool(IsCasting3, false);
                    break;
                case State.AutoAttack:
                    animator.SetBool(IsMoving, false);
                    animator.SetBool(DoesBaseAttack, true);
                    animator.SetBool(IsCasting1, false);
                    animator.SetBool(IsCasting2, false);
                    animator.SetBool(IsCasting3, false);
                    break;
                case State.Attack1:
                    animator.SetBool(IsMoving, false);
                    animator.SetBool(DoesBaseAttack, false);
                    animator.SetBool(IsCasting1, true);
                    animator.SetBool(IsCasting2, false);
                    animator.SetBool(IsCasting3, false);
                    break;
                case State.Attack2:
                    animator.SetBool(IsMoving, false);
                    animator.SetBool(DoesBaseAttack, false);
                    animator.SetBool(IsCasting1, false);
                    animator.SetBool(IsCasting2, true);
                    animator.SetBool(IsCasting3, false);
                    break;
                case State.Attack3:
                    animator.SetBool(IsMoving, false);
                    animator.SetBool(DoesBaseAttack, false);
                    animator.SetBool(IsCasting1, false);
                    animator.SetBool(IsCasting2, false);
                    animator.SetBool(IsCasting3, true);
                    break;
                case State.Blocked:
                    animator.SetBool(IsMoving, false);
                    animator.SetBool(DoesBaseAttack, false);
                    animator.SetBool(IsCasting1, false);
                    animator.SetBool(IsCasting2, false);
                    animator.SetBool(IsCasting3, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        #endregion

        #region Public Methods

        public void Init(PlayerModel.Scripts.PlayerModel player, Animator commanderAnimator)
        {
            player.OnPlayerStateChanged += ChangeAnimation;
            animator = commanderAnimator;
        }

        #endregion
    }
}
