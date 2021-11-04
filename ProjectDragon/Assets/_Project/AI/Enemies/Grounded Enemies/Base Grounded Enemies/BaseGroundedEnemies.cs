using AI.Enemies.Base_Enemy;
using Dreamteck.Splines;
using GamePlay.GameManager.Scripts;
using UnityEngine;

namespace AI.Enemies.Grounded_Enemies.Base_Grounded_Enemies
{
    public class BaseGroundedEnemies : Enemy
    {
        [SerializeField] private SplineFollower follower;

        private void Start()
        {
            // Only for Beta, will be done in the enemy spawner for multiple splines
            SetSpline(GameManager.Instance.SplineComputer);
        }

        /// <summary>
        /// Walks to the HQ.
        /// </summary>
        public void WalkToHq()
        {
            follower.followSpeed = speed;
            follower.followMode = SplineFollower.FollowMode.Uniform;
        }

        /// <summary>
        /// Sets the spline computer of the Grounded Enemy
        /// </summary>
        /// <param name="splineComputer"> The Spline Computer. </param>
        public void SetSpline(SplineComputer splineComputer)
        {
            follower.spline = splineComputer;
        }
    }
}
