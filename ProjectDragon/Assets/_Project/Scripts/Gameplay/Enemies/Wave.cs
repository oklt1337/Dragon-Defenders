using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Enemies
{
    public class Wave : MonoBehaviour
    {
       [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    }
}
