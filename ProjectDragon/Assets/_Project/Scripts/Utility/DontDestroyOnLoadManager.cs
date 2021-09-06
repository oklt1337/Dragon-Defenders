using UnityEngine;

namespace _Project.Scripts.Utility
{
    public class DontDestroyOnLoadManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
