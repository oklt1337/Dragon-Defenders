using UnityEngine;

namespace Utility.DontDestroyOnLoad.Scripts
{
    public class DontDestroyOnLoadManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
