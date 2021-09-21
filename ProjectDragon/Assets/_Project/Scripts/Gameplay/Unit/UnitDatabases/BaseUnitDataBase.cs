using System.IO;
using _Project.Scripts.Gameplay.Faction;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Unit.UnitDatabases
{
    [CreateAssetMenu(menuName="Tools/Units/BaseUnitDataBase")]
    public class BaseUnitDataBase : ScriptableObject
    {
        public string unitName;
        public Factions.Faction faction;
        public Factions.Class unitClass;
        public byte rank;
        public int cost;
        
        public float TestTest;
    }
}
