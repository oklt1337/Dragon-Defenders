using UnityEngine;
using static _Project.GamePlay.GameManager.Scripts.GameManager;

namespace _Project.Scripts.Gameplay.Enemies
{
    public class Runner : Mover
    {
        /// <summary>
        /// Walks to the Base.
        /// </summary>
        public void WalkToBase()
        {
            agent.SetDestination(Instance.Hq.transform.position);
        }
    }
}
