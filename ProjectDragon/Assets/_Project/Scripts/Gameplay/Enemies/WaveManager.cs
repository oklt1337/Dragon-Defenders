using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Enemies
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private List<Wave> waves = new List<Wave>();

        private int _currentWave;

        public void SpawnNextWave()
        {
            _currentWave++;
        }
    }
}
