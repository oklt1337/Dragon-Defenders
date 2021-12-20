using System;
using System.Collections.Generic;
using System.Linq;
using AbilitySystem.ActorSelector.Scripts;
using AbilitySystem.Condition.Scripts;
using AbilitySystem.Feedback.Scripts;
using UnityEditor;
using UnityEngine;

namespace _Project.Utility.Editor.AbilityCreator.Scripts
{
    public class AbilityCreator : EditorWindow
    {
        private static AbilityCreator Instance;

        [Header("General")] 
        private GameObject projectile;

        [Header("Modifier")]
        [Header("Context")]
        private bool showContext;
        private bool hasDuration;
        private float duration;
        private List<Condition> conditions = new List<Condition>();
        
        [Header("Condition")]
        private bool showCondition;
        private ConditionType conditionType;
        private float conditionValue;
        
        [Header("Feedback")]
        private bool showFeedback;
        private List<Feedback> feedbacks = new List<Feedback>();
        private FeedbackType feedbackType;

        [Header("ActorSelector")] 
        private ActorSelector actorSelector;
        private List<ValidTargets> validTargetsList = new List<ValidTargets>();

        [MenuItem("Window/Custom/Ability Creator")]
        public static void Init()
        {
            Instance = GetWindow<AbilityCreator>("Ability Creator");
            Instance.Show();
        }

        private void OnValidate()
        {

        }

        private void OnGUI()
        {
            DrawContext();
            DrawFeedback();
            DrawActorSelector();
        }

        private void DrawActorSelector()
        {
            
        }

        private void DrawFeedback()
        {
            showFeedback = EditorGUILayout.Foldout(showFeedback, "Feedback");
            if (!showFeedback) 
                return;
            feedbackType = (FeedbackType) EditorGUILayout.EnumPopup("Feedback Type", feedbackType);
        }
        
        private void DrawContext()
        {
            showContext = EditorGUILayout.Foldout(showContext, "Context");
            if (!showContext) 
                return;
            hasDuration = EditorGUILayout.Toggle("Has Duration", hasDuration);
            if (hasDuration)
                duration = EditorGUILayout.FloatField("Duration", duration);
            DrawCondition();
        }

        private void DrawCondition()
        {
            showCondition = EditorGUILayout.Foldout(showCondition, "Condition");
            if (!showCondition) 
                return;
            conditionType = (ConditionType) EditorGUILayout.EnumPopup("Condition Type", conditionType);
            conditionValue = EditorGUILayout.FloatField("Value", conditionValue);
        }
    }
}