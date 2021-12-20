using System;

namespace AbilitySystem.Effects.BaseEffects.Scripts
{
    [Serializable]
    public abstract class Effect
    {
        public Type.Scripts.Type type;
        public Intensity.Scripts.Intensity intensity;

        public abstract void ApplyEffect(Entity.Scripts.Entity entity);
    }
}