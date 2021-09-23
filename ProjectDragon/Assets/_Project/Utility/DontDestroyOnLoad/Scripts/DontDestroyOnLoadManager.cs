using UnityEngine;

namespace _Project.Utility.DontDestroyOnLoad.Scripts
{
    public class DontDestroyOnLoadManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
