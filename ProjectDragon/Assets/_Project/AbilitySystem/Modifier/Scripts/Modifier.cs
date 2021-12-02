using System.Collections.Generic;
using AbilitySystem.Effects.Scripts;

namespace AbilitySystem.Modifier.Scripts
{
    public class Modifier
    {
        public Context.Scripts.Context Context;
        public List<Feedback.Scripts.Feedback> Feedbacks = new List<Feedback.Scripts.Feedback>();
        public ActorSelector.Scripts.ActorSelector ActorSelector;
        public List<Effect> Effects = new List<Effect>();
    }
}