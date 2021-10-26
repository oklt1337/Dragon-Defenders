using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

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

        [SerializeField] private Vector3 testSpawnPosition = new Vector3(13,0,0);
        [Button]
        public void PlaceUnitProxy()
        {
            Unit.BaseUnits.Unit unit = Units[UnityEngine.Random.Range(0,Units.Count-1)];
            
            PhotonNetwork.Instantiate(String.Concat(unitPath, unit.name), testSpawnPosition, Quaternion.identity);
        }
        
        
        
        [Button]
        public void EnableSkillTest0()
        {
            if (ActiveUnits[0].SkillTree.EnableSkillThroughSkillTree("0"))
            {
                Debug.Log("SkillEnabled EnableSkillTest0");
            };
        }
        [Button]
        public void EnableSkillTest1()
        {
            if (ActiveUnits[0].SkillTree.EnableSkillThroughSkillTree("1"))
            {
                Debug.Log("SkillEnabled EnableSkillTest1");
            };
        }
        [Button]
        public void EnableSkillTest2()
        {
            if (ActiveUnits[0].SkillTree.EnableSkillThroughSkillTree("2"))
            {
                Debug.Log("SkillEnabled EnableSkillTest2");
            };
        }
        [Button]
        public void EnableSkillTest3()
        {
            if (ActiveUnits[0].SkillTree.EnableSkillThroughSkillTree("3"))
            {
                Debug.Log("SkillEnabled EnableSkillTest3");
            };
        }
        [Button]
        public void EnableSkillTest4()
        {
            if (ActiveUnits[0].SkillTree.EnableSkillThroughSkillTree("4"))
            {
                Debug.Log("SkillEnabled EnableSkillTest4");
            };
        }
        
        #endregion
    
        #region CallBacks


        #endregion
    }
}
