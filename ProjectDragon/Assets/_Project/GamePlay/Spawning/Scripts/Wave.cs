using System.Collections.Generic;
using _Project.AI.Enemies.Scripts;
using _Project.Enemies.Scripts;
using UnityEngine;

namespace _Project.GamePlay.Spawning.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Wave")]
    public class Wave : ScriptableObject
    {
       [SerializeField] private List<GameObject> enemies = new List<GameObject>();

       public List<GameObject> Enemies => enemies;
    }
}
