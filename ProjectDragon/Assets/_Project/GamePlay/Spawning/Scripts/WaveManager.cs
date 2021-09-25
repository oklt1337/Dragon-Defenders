using System;
using System.Collections.Generic;
using _Project.GamePlay.GameManager.Scripts;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Enemies
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private List<Wave> waves = new List<Wave>();

        private int _currentWave;

        private void Awake()
        {
            GameManager.Instance.EnemySpawner.OnWaveSucces += SpawnNextWave;
        }

        private void SpawnNextWave()
        {
            _currentWave++;
        }
    }
}
