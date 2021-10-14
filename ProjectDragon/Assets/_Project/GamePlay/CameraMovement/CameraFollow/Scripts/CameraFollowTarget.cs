using UnityEngine;

namespace _Project.GamePlay.CameraMovement.CameraFollow.Scripts
{
    /// <summary>
    /// Author: Christopher Zelch
    /// </summary>
    public class CameraFollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private const float ZOffset = -3.5f;

        private void Start()
        {
            target = GameManager.Scripts.GameManager.Instance.PlayerModel.transform;
        }

        private void Update()
        {
            OnTargetMoved();
        }

        /// <summary>
        /// Camera follows target.
        /// </summary>
        private void OnTargetMoved()
        {
            Transform cameraTransform = transform;
            Vector3 cameraPosition = cameraTransform.position;
            Vector3 targetPosition = target.position;
            cameraPosition = new Vector3(targetPosition.x, cameraPosition.y, targetPosition.z + ZOffset);
            cameraTransform.position = cameraPosition;
        }
    }
}
