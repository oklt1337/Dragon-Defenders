using UnityEngine;

namespace _Project.Scripts
{
    public class DontDestroyOnLoadManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
