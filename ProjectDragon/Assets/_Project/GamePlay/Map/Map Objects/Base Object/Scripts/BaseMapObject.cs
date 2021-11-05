using UnityEngine;

namespace GamePlay.Map.Map_Objects.Base_Object.Scripts
{
    public class BaseMapObject : MonoBehaviour
    {
        [Header("Mesh stuff")]
        [SerializeField] protected MeshFilter meshFilter;
        [SerializeField] protected Mesh goodMesh;
        [SerializeField] protected Mesh evilMesh;
        [SerializeField] protected Mesh neutralMesh;
    }
}
