using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AbilitySystem.Context.Scripts
{
    public class Context : ScriptableObject
    {
        public bool isActive;
        public bool hasDuration;
        public float duration;
        public readonly List<Condition.Scripts.Condition> Conditions = new List<Condition.Scripts.Condition>();

        /// <summary>
        /// Checks Conditions if all conditions are true and !isActive;
        /// </summary>
        /// <returns>All conditions are true and !isActive</returns>
        public bool CheckConditions()
        {
            if (hasDuration && duration < 0)
                return false;

            return !isActive && Conditions.All(condition => condition.CheckCondition());
        }
    }
}