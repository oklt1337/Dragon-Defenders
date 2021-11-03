using AI.Enemies.Grounded_Enemies.Base_Grounded_Enemies;
using UnityEngine;

namespace AI.Enemies.Grounded_Enemies.Runner.Scripts
{
    public class Runner : BaseGroundedEnemies
    {
        [SerializeField] private float speedUpValue;

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

        private void SpeedUp()
        {
            speed = speedUpValue;
        }

        #endregion

        #region Overrides

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            if (health * 2 <= maxHealth)
                SpeedUp();
        }

        #endregion
    }
}