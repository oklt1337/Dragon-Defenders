using GamePlay.GameManager.Scripts;
using UnityEngine;

namespace GamePlay.CommanderWaypoint.Scripts
{
    public class CommanderMoveIndicator : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        private GameObject _initializedObj;
        
        private void Awake()
        {
            GameManager.Scripts.GameManager.Instance.OnGameStateChanged += DeletePoint;
        }

        private void DeletePoint()
        {
            if (_initializedObj != null)
            {
                Destroy(_initializedObj);
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
            _initializedObj =  Instantiate(prefab, position, Quaternion.identity, transform);
        }
    }
}
