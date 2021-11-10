using GamePlay.GameManager.Scripts;
using UnityEngine;

namespace GamePlay.CommanderWaypoint.Scripts
{
    public class CommanderMoveIndicator : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        private GameObject initializedObj;
        
        private void Awake()
        {
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += DeletePoint;
        }

        private void DeletePoint()
        {
            if (initializedObj != null)
            {
                Destroy(initializedObj);
            }
        }
        
        private void DeletePoint(GameState state)
        {
            if (state == GameState.Wave)
                return;
            
            DeletePoint();
        }

        public void InitializeMovePoint(Vector3 position)
        {
            DeletePoint();
            initializedObj =  Instantiate(prefab, position, Quaternion.identity, transform);
        }
    }
}
