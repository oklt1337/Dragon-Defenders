using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Units.Unitmanager
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class UnitManager : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private List<Unit.BaseUnits.Unit> units;

        #endregion

        #region Private Fields

        private const string UnitPath = "Units/";

        #endregion

        #region Public Properties

        public List<Unit.BaseUnits.Unit> Units
        {
            get => units;
            set => units = value;
        }

        public List<Unit.BaseUnits.Unit> ActiveUnits { get; set; }

        #endregion

        #region Unity Methods

        private void Start()
        {
            units ??= new List<Unit.BaseUnits.Unit>();

            ActiveUnits = new List<Unit.BaseUnits.Unit>();
        }

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
            PhotonNetwork.Instantiate(string.Concat(UnitPath, unit.name), spawnPoint, Quaternion.identity);
        }

        #endregion
    }
}