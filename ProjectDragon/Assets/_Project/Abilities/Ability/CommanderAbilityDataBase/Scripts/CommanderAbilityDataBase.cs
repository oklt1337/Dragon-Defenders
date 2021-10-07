using System;
using System.Collections.Generic;
using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _Project.Abilities.Ability.CommanderAbilityDataBase.Scripts
{
    [CreateAssetMenu (menuName = "Tools/CommanderAbilityDataBase", fileName = "CommanderAbilityDataBase")] 
    public class CommanderAbilityDataBase : SerializedScriptableObject
    {
        
        // Notation fuer Liste:
        // _Project.Abilities.Ability.EndAbilities.CombatAbilities.FireBallHoming.FireBallHoming, Assembly-CSharp
        [OdinSerialize] public List<Type> CommanderAbilitiesScripts = new List<Type>();
        public List<AbilityDataBase> commanderAbilitiesDataBases = new List<AbilityDataBase>();
    }
}


