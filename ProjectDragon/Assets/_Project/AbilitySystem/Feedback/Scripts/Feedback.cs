using UnityEngine;

namespace AbilitySystem.Feedback.Scripts
{
    public abstract class Feedback : ScriptableObject
    {
        public FeedbackType feedbackType;
        public abstract void ProcessFeedback();
    }
}