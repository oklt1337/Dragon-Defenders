using _Project.Enemies.Scripts;
using UnityEngine;

namespace _Project.GamePlay.Map.Scripts
{
    public class WayPoint : MonoBehaviour
    {
        [SerializeField] private GameObject[] possiblePoints; 
        
        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.CompareTag("Moving Enemy"))
                return;

            var mover = other.gameObject.GetComponent<Runner>();
            mover.UpdateWayPoint(possiblePoints[Random.Range(0, possiblePoints.Length)].transform.position);
        }
    }
}