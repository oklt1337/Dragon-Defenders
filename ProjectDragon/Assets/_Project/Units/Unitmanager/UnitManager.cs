using System;
using System.Collections.Generic;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Units.Unitmanager
{
    public class UnitManager : MonoBehaviour
    {
        [SerializeField] private List<Unit.BaseUnits.Unit> units;
        private String unitPath = "Resources/Units/";
        public List<Unit.BaseUnits.Unit> Units
        {
            get => units;
            set => units = value;
        }

        private void PlaceUnit(Unit.BaseUnits.Unit unit, Vector3 spawnPoint)
        {
            PhotonNetwork.Instantiate(String.Concat(unitPath, unit.name), spawnPoint, Quaternion.identity);
        }
    }
}
