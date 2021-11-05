using AI.Enemies.Base_Enemy;
using Dreamteck.Splines;
using UnityEngine;

namespace AI.Enemies.Grounded_Enemies.Base_Grounded_Enemies.Scripts
{
    public class BaseGroundedEnemies : Enemy
    {
        [SerializeField] protected SplineFollower follower;

        public SplineFollower Follower => follower;
        
        private void Start()
        {
            follower.followSpeed = speed;
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
