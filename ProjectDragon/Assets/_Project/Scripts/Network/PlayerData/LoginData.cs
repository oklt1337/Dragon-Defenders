using UnityEngine;

namespace _Project.Scripts.Network.PlayerData
{
    [CreateAssetMenu(menuName = "Tools/UserData/loginData")]
    public class LoginData : ScriptableObject
    {
        public bool autoLogin;
    }
}
