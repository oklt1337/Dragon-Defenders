using System;
using System.Collections.Generic;
using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _Project.Abilities.Ability.CommanderAbilityDataBase.Scripts
{
    /// <summary>
    /// Author: Christopher Zelch
    /// </summary>
    [CreateAssetMenu (menuName = "Tools/CommanderAbilityDataBase", fileName = "CommanderAbilityDataBase")] 
    public class CommanderAbilityDataBase : SerializedScriptableObject
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields

    

        #endregion
    
        #region Public Fields

        [OdinSerialize] public List<Type> CommanderAbilitiesScripts = new List<Type>();
        public List<AbilityDataBase> commanderAbilitiesDataBases = new List<AbilityDataBase>();

        #endregion
    
        #region Public Properties

    

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

    

        #endregion
    
        #region CallBacks


        #endregion
        
    }
}


