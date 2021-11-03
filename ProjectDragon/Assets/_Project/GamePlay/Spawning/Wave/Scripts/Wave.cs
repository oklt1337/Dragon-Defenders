using System.Collections.Generic;
using System.Linq;
using AI.Enemies.Base_Enemy;
using UnityEngine;

namespace _Project.GamePlay.Spawning.Wave.Scripts
{
    [CreateAssetMenu(menuName = "Tools/Wave")]
    public class Wave : ScriptableObject
    {
       [SerializeField] private List<Enemy> enemies = new List<Enemy>();

       public List<Enemy> Enemies => enemies;

       public int WaveCombatScore
       {
           get
           {
               return Enemies.Sum(enemy => enemy.EnemyCombatScore);
           }
       }
    }
}
