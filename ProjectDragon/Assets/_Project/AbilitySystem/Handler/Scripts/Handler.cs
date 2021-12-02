using System.Collections.Generic;
using AbilitySystem.Effects.Scripts;

namespace AbilitySystem.Handler.Scripts
{
    public abstract class Handler
    {
        private Entity.Scripts.Entity entity;
        public List<Effect> Effects { get; } = new List<Effect>();
        
        public Handler(Entity.Scripts.Entity entity)
        {
            this.entity = entity;
        }

        public void AddEntry(Effect effect)
        {
            Effects.Add(effect);
            ProcessEntries();
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