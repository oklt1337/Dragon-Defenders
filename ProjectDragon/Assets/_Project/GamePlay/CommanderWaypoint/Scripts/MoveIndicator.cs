using UnityEngine;

namespace _Project.GamePlay.CommanderWaypoint.Scripts
{
    public class MoveIndicator : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
