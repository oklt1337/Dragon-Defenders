using System;

namespace AbilitySystem.Feedback.Scripts
{
    [Serializable]
    public abstract class Feedback
    {
        public FeedbackType feedbackType;
        public abstract void ProcessFeedback();
    }
}