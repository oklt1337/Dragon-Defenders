using System;

namespace AbilitySystem.ActorSelector.Scripts
{
    [Flags]
    public enum ValidTargets
    {
        Player = 1,
        Unit = 2,
        Enemy = 4,
        Breakables = 8,
        Environment = 16,
        All = ~0
    }
}