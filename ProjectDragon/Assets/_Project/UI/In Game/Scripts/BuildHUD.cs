using _Project.GamePlay.GameManager.Scripts;
using _Project.UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.In_Game.Scripts
{
    public class BuildHUD : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button startWaveButton;

        public void ChangeInteractableStatus(bool status)
        {
            startWaveButton.interactable = status;
        }

        public void OnClickStartWave()
        {
            // TODO: ChangeHUD
            GameManager.Instance.ChangeState(GameState.Wave);
            GameManager.Instance.WaveManager.SpawnNextWave();
        }
    }
}
