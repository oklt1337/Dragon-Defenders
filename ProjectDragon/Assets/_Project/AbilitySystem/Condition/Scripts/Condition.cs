using System;
using UnityEngine;

namespace AbilitySystem.Condition.Scripts
{
    public abstract class Condition : ScriptableObject
    {
        public ConditionType conditionType;
        public abstract bool CheckCondition();
    }
}