using System;

namespace AbilitySystem.Condition.Scripts
{
    public enum ConditionEnum
    {
        None,
        Default
    }
    
    [Serializable]
    public abstract class Condition
    {
        public ConditionType conditionType;
        public abstract bool CheckCondition();
    }
}