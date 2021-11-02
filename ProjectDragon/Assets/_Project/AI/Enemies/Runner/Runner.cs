using _Project.AI.Enemies.Scripts;
using _Project.AI.FSM.Scripts;
using UnityEngine;

namespace _Project.AI.Enemies.Runner
{
    public class Runner : Mover
    {
        #region Serialized Fields

        [SerializeField] private Vector3 currentWayPoint;

        #endregion


        public FiniteStateMachine Fsm { get; private set; }

        #region Unity Methods
        
        private void Awake()
        {
            Fsm = new FiniteStateMachine(this);

            Fsm.Initialize(Fsm.RunAtTargetState);
        }

        private void FixedUpdate()
        {
            Fsm.Update();
        }

        #endregion


        #region Publid Methods

        /// <summary>
        /// Updates the current waypoint.
        /// </summary>
        /// <param name="newWayPoint"> The new point the mover shall go to. </param>
        public void UpdateWayPoint(Vector3 newWayPoint)
        {
            currentWayPoint = newWayPoint;
        }

        #endregion
    }
}