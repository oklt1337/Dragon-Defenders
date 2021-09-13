using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Player
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private Camera buildCamera;
        [SerializeField] private Camera commanderCamera;
        
        private int _money;
        private Commander _commander;

        private void Awake()
        {
            //change camera event 
        }

        private void ChangeCamera(GameState state)
        {
            switch (state)
            {
                case GameState.Build:
                    buildCamera.gameObject.SetActive(true);
                    commanderCamera.gameObject.SetActive(false);
                    break;
                case GameState.Wave:
                    buildCamera.gameObject.SetActive(false);
                    commanderCamera.gameObject.SetActive(true);
                    break;
                case GameState.Prepare:
                    Debug.LogError($"GameState: {state}");
                    break;
                case GameState.End:
                    Debug.LogError($"GameState: {state}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
