using System;

namespace AbilitySystem.Intensity.Scripts
{
    [Serializable]
    public abstract class Intensity
    {
        public IntensityType intensityType;
        public float value;
    }
}