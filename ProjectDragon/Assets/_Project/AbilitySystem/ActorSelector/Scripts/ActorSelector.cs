using System.Collections.Generic;

namespace AbilitySystem.ActorSelector.Scripts
{
    public abstract class ActorSelector
    {
        public readonly List<ValidTargets> Targets = new List<ValidTargets>();
        public bool ValidateTarget(ValidTargets target)
        {
            return Targets.Contains(target);
        }
    }
}