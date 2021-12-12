using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Serialization;

namespace AbilitySystem.Context.Scripts
{
    [Serializable]
    public class Context
    {
        public bool isActive;
        public bool hasDuration;
        public float duration;
        [OdinSerialize] public List<Condition.Scripts.Condition> Conditions = new List<Condition.Scripts.Condition>();

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