using System.Collections.Generic;
using Photon.Pun;
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

        private void PlaceUnit(string unitString, Vector3 spawnPoint)
        {
            //Instantiate(unit.gameObject, spawnPoint,Quaternion.identity, this.transform);
            PhotonNetwork.Instantiate(unitString, spawnPoint, Quaternion.identity);
        }
        
        
    }
}
