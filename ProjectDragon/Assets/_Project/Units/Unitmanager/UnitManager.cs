using System;
using System.Collections.Generic;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Units.Unitmanager
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

        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods

    

        #endregion
    
        #region Private Methods

    

        #endregion
    
        #region Protected Methods

    

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
