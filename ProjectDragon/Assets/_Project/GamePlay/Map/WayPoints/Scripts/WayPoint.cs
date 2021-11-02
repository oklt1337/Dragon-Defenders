using _Project.AI.Enemies.Runner;
using _Project.AI.Enemies.Scripts;
using UnityEngine;

namespace _Project.GamePlay.Map.WayPoints.Scripts
{
    public class WayPoint : MonoBehaviour
    {
        [SerializeField] private GameObject[] possiblePoints; 
        
        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.CompareTag("Enemy"))
                return;

            var mover = other.gameObject.GetComponent<Runner>();
            mover.UpdateWayPoint(possiblePoints[Random.Range(0, possiblePoints.Length)].transform.position);
        }
    }
}