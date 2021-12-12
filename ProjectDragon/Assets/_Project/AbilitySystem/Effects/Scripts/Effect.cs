using System;

namespace AbilitySystem.Effects.Scripts
{
    [Serializable]
    public abstract class Effect
    {
        public Type.Scripts.Type Type;
        public Intensity.Scripts.Intensity Intensity;
        public abstract void ApplyEffect(Entity.Scripts.Entity entity);
    }
}