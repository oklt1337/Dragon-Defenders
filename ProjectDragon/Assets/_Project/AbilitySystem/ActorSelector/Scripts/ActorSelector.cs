using System;
using System.Collections.Generic;
using Sirenix.Serialization;

namespace AbilitySystem.ActorSelector.Scripts
{
    [Serializable]
    public abstract class ActorSelector
    {
        [OdinSerialize] public List<ValidTargets> targets = new List<ValidTargets>();
        public abstract bool ValidateTarget(ValidTargets target);
    }
}