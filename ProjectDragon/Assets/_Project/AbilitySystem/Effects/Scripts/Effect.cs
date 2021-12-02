using UnityEngine;

namespace AbilitySystem.Effects.Scripts
{
    public abstract class Effect : ScriptableObject
    {
        public Type.Scripts.Type Type;
        public Intensity.Scripts.Intensity Intensity;
        public abstract void ApplyEffect(Entity.Scripts.Entity entity);
    }
}