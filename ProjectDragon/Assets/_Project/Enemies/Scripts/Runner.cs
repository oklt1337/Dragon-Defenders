using _Project.Scripts.Gameplay.Enemies;
using static _Project.GamePlay.GameManager.Scripts.GameManager;

namespace _Project.Enemies.Scripts
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
