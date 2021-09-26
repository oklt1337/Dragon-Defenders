using System.Collections.Generic;
using _Project.Enemies.Scripts;
using UnityEngine;

namespace _Project.GamePlay.Spawning.Scripts
{
    public class Wave : MonoBehaviour
    {
       [SerializeField] private List<Enemy> enemies = new List<Enemy>();

       public List<Enemy> Enemies => enemies;
    }
}
