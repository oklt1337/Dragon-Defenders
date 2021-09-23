using UnityEngine;

namespace _Project.Network.PlayerData.Scripts
{
    [CreateAssetMenu(menuName = "Tools/UserData/loginData")]
    public class LoginData : ScriptableObject
    {
        public bool autoLogin;
    }
}
