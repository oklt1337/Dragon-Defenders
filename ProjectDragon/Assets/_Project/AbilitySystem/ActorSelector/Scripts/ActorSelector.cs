using System;
using UnityEngine;

namespace AbilitySystem.ActorSelector.Scripts
{
    [Serializable]
    public abstract class ActorSelector
    {
        [SerializeField] private ValidTargets targets;
        public abstract bool ValidateTarget(ValidTargets target);
    }
}