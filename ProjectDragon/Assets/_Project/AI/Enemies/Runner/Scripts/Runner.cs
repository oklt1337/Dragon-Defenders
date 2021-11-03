using AI.Enemies.Scripts;

namespace AI.Enemies.Runner.Scripts
{
    public class Runner : Mover
    {
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

        public void WalkToHq()
        {
            
        }
    }
}