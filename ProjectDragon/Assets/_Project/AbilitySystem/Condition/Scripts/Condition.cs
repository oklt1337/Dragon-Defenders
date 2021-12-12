using System;

namespace AbilitySystem.Condition.Scripts
{
    [Serializable]
    public abstract class Condition
    {
        public ConditionType conditionType;
        public abstract bool CheckCondition();
    }
}