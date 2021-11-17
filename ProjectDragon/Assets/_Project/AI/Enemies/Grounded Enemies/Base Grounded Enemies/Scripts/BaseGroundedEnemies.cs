using AI.Enemies.Base_Enemy;
using AI.Enemies.Base_Enemy.Scripts;
using Dreamteck.Splines;
using GamePlay.GameManager.Scripts;
using UnityEngine;

namespace AI.Enemies.Grounded_Enemies.Base_Grounded_Enemies.Scripts
{
    public abstract class BaseGroundedEnemies : Enemy
    {
        [SerializeField] protected SplineFollower follower;

        public SplineFollower Follower => follower;
        
        private void Start()
        {
            follower.followSpeed = speed;
            
            // Only for Beta, will be updated for multiple splines.
            follower.spline = GameManager.Instance.SplineComputer;
        }

        /// <summary>
        /// Walks to the HQ.
        /// </summary>
        public void WalkToHq()
        {
            follower.followMode = SplineFollower.FollowMode.Uniform;
        }
    }
}
