using System.Collections.Generic;
using UnityEngine;

namespace _Project.GamePlay.Spawning.Wave.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Wave")]
    public class Wave : ScriptableObject
    {
       [SerializeField] private List<GameObject> enemies = new List<GameObject>();

       public List<GameObject> Enemies => enemies;
    }
}
