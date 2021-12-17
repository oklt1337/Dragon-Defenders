using System.Collections.Generic;
using AbilitySystem.Effects.Scripts;

namespace AbilitySystem.Handler.Scripts
{
    public class Handler
    {
        private readonly Entity.Scripts.Entity entity;
        public List<Effect> Effects { get; } = new List<Effect>();

        public bool HasEntries => Effects.Count > 0;
        
        public Handler(Entity.Scripts.Entity entity)
        {
            this.entity = entity;
        }

        public void AddEntry(Effect effect)
        {
            Effects.Add(effect);
        }

        public void ProcessEntries()
        {
            foreach (var effect in Effects)
            {
                effect.ApplyEffect(entity);
            }
        }
    }
}