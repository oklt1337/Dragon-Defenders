using System;
using UnityEngine;

namespace _Project.GamePlay.CommanderWaypoint.Scripts
{
    public class CommanderMoveIndicator : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        private GameObject _initializedObj;

        private void DeletePoint()
        {
            if (_initializedObj != null)
            {
                Destroy(_initializedObj);
            }
        }

        public void InitializeMovePoint(Vector3 position)
        {
            DeletePoint();
            _initializedObj =  Instantiate(prefab, position, Quaternion.identity, transform);
        }
    }
}
