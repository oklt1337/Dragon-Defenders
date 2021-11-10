using AI.Enemies.Grounded_Enemies.Base_Grounded_Enemies.Scripts;
using UnityEngine;

namespace AI.Enemies.Grounded_Enemies.Runner.Scripts
{
    public class Runner : BaseGroundedEnemies
    {
        [SerializeField] private float speedUpValue;

        private float stunValue;
        private bool hasBeenBoosted;

        public RunnerFsm Fsm { get; private set; }


        #region Unity Methods

        private void Awake()
        {
            Fsm = new RunnerFsm(this);

            Fsm.Initialize(Fsm.RunToHqState);
        }

        private void FixedUpdate()
        {
            Fsm.Update();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Increases the runners speed by its speedUpValue.
        /// </summary>
        private void SpeedUp()
        {
            follower.followSpeed += speedUpValue;

            hasBeenBoosted = true;
        }

        #endregion

        #region Overrides

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            if (health * 2 <= maxHealth && !hasBeenBoosted)
                SpeedUp();
        }

        public override void Stun(float stunTime)
        {
            base.Stun(stunTime);
            Fsm.Transition(Fsm.EndureStunState);
        }

        #endregion
    }
}