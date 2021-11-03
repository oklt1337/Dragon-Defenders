using System.Collections.Generic;
using Abilities.Ability.Scripts;
using UnityEngine;

namespace Abilities.AbilityDataBase.Scripts
{
    public class AbilityDataBase : ScriptableObject
    {
        [SerializeField] private List<AbilityObj> abilities = new List<AbilityObj>();

        public List<AbilityObj> Abilities => abilities;
    }
}
