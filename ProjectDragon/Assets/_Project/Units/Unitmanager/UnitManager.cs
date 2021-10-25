using System;
using System.Collections.Generic;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Units.UnitManager
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class UnitManager : MonoBehaviour
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

        [SerializeField] private List<Unit.BaseUnits.Unit> units;

        #endregion
    
        #region Private Fields

        private string unitPath = "Units/";

        #endregion
    
        #region protected Fields

        

        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties

        public List<Unit.BaseUnits.Unit> Units
        {
            get => units;
            set => units = value;
        }
        
        public List<Unit.BaseUnits.Unit> ActiveUnits { get; set; }

        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods

        private void Start()
        {
            if (units == null)
            {
                units = new List<Unit.BaseUnits.Unit>();
            }
            ActiveUnits = new List<Unit.BaseUnits.Unit>();
        }
        
        

        #endregion
    
        #region Private Methods

    

        #endregion
    
        #region Protected Methods

        public void SubscribeUnit(Unit.BaseUnits.Unit unit)
        {
            ActiveUnits.Add(unit);
        }
        
        public void UnsubscribeUnit(Unit.BaseUnits.Unit unit)
        {
            ActiveUnits.Remove(unit);
        }

        #endregion
    
        #region Public Methods

        public void PlaceUnit(Unit.BaseUnits.Unit unit, Vector3 spawnPoint)
        {
            PhotonNetwork.Instantiate(String.Concat(unitPath, unit.name), spawnPoint, Quaternion.identity);
        }

        #endregion
    
        #region CallBacks


        #endregion
    }
}
