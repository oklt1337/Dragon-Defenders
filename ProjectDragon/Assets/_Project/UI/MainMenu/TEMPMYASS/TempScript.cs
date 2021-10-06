using _Project.Network.NetworkManager.Scripts;
using _Project.Network.Photon.Scripts;
using _Project.Utility.SceneManager.Scripts;
using Photon.Pun;
using UnityEngine;

namespace _Project.UI.MainMenu.TEMPMYASS
{
    public class TempScript : MonoBehaviour
    {
        public void OnClickPlay()
        {
            NetworkManager.Instance.PhotonManager.PhotonConnector.CreateRoom();
            SceneManager.ChangeScene(Scene.GameScene);
        }
    }
}
