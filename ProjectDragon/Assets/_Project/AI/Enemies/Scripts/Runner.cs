using System;
using _Project.Enemies.Scripts;
using UnityEngine;

namespace _Project.AI.Enemies.Scripts
{
    public class Runner : Mover
    {
        #region Serialized Fields

        [SerializeField] private Vector3 currentWayPoint;

        #endregion

        private void Update()
        {
            WalkToBase();
        }

        #region Publid Methods

        /// <summary>
        /// Walks to the Base.
        /// </summary>
        public void WalkToBase()
        { 
            agent.SetDestination(currentWayPoint);
        }
                
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
