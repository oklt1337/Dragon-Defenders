using _Project.GamePlay.GameManager.Scripts;

namespace _Project.Enemies.Scripts
{
    public class Flier : Mover
    {
        /// <summary>
        /// Flies to the Base.
        /// </summary>
        public void FlyToBase()
        {
            agent.SetDestination(GameManager.Instance.Hq.transform.position);
        }
    }
}
