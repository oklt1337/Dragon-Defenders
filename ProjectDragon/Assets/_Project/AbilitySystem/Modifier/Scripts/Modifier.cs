using System;
using System.Collections.Generic;
using AbilitySystem.Effects.BaseEffects.Scripts;
using AbilitySystem.Effects.Scripts;
using Sirenix.Serialization;

namespace AbilitySystem.Modifier.Scripts
{
    [Serializable]
    public class Modifier
    {
        [OdinSerialize] public Context.Scripts.Context context;
        [OdinSerialize] public ActorSelector.Scripts.ActorSelector actorSelector;
        [OdinSerialize] public List<Feedback.Scripts.Feedback> feedbacks = new List<Feedback.Scripts.Feedback>();
        [OdinSerialize] public List<Effect> effects = new List<Effect>();
    }
}