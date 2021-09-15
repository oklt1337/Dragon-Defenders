using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Unit
{
    public class UnitManager : MonoBehaviour
    {
        [SerializeField] private List<Unit> units;

        public List<Unit> Units
        {
            get => units;
            set => units = value;
        }

        private void PlaceUnit(Unit unit, Vector3 spawnPoint)
        {   //photon instanziert;
            Instantiate(unit.gameObject, spawnPoint,Quaternion.identity, this.transform);
        }
        
        
    }
}
