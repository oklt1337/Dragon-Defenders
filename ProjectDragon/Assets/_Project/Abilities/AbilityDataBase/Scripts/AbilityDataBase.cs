using System.Collections.Generic;
using UnityEngine;

namespace _Project.Abilities.AbilityDataBase.Scripts
{
    public class AbilityDataBase : ScriptableObject
    {
        [SerializeField] private List<Ability.Scripts.Ability> abilities = new List<Ability.Scripts.Ability>();

        public List<Ability.Scripts.Ability> Abilities => abilities;
    }
}
