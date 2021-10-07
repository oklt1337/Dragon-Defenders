using System;
using System.Collections.Generic;
using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _Project.Abilities.Ability.CommanderAbilityDataBase.Scripts
{
    [CreateAssetMenu (menuName = "Tools/UnitAbilityDataBase")]
    public class UnitAbilityDataBase : SerializedScriptableObject
    {
        [OdinSerialize] public Type UnitAbilitiesScript;
        public AbilityDataBase unitAbilitiesDataBase;
    }
}
