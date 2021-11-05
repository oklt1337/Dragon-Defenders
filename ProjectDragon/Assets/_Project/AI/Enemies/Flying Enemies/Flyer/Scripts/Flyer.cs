using AI.Enemies.Flying_Enemies.Base_Flying_Enemies.Scripts;

namespace AI.Enemies.Flying_Enemies.Flyer.Scripts
{
    public class Flyer : BaseFlyingEnemy
    {
        public FlyerFsm Fsm { get; private set; }

        private void Awake()
        {
            Fsm = new FlyerFsm(this);
            
            Fsm.Initialize(Fsm.FlyToHqState);
        }
    }
}
