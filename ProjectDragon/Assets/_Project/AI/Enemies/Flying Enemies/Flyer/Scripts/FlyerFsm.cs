using AI.Enemies.Base_Enemy;
using AI.FSM.Scripts;
using AI.States.Base_Flying_Enemies;
using UnityEngine;

namespace AI.Enemies.Flying_Enemies.Flyer.Scripts
{
    public class FlyerFsm : FiniteStateMachine
    {
        private Flyer owner;
        
        public State FlyToHqState { get; }
        
        public FlyerFsm(Flyer newOwner) : base(newOwner)
        {
            owner = newOwner;
            FlyToHqState = new FlyToHqState(this, owner);
        }
    }
}
