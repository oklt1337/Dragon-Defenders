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
        #region Public Fields

        [OdinSerialize] public List<Type> CommanderAbilitiesScripts = new List<Type>();
        public List<AbilityDataBase> commanderAbilitiesDataBases = new List<AbilityDataBase>();

        #endregion
    }
}


