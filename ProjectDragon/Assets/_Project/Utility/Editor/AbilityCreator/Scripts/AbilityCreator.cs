using System;
using System.Collections.Generic;
using AbilitySystem.ActorSelector.Scripts;
using AbilitySystem.Condition.Scripts;
using AbilitySystem.Feedback.Scripts;
using UnityEditor;
using UnityEngine;

namespace _Project.Utility.Editor.AbilityCreator.Scripts
{
    internal enum WindowMode
    {
        Default,
        CreateCondition
    }
    
    public class AbilityCreator : EditorWindow
    {
        private static AbilityCreator Instance;

        [Header("General")] 
        private GameObject projectile;
        private static WindowMode WindowMode;

        [Header("Modifier")]
        [Header("Context")]
        private bool showContext;
        private bool hasDuration;
        private float duration;
        private List<Condition> conditions = new List<Condition>();

        [Header("Condition")] 
        private ConditionEnum conditionEnum;
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
            WindowMode = WindowMode.Default;
            Instance.Show();
        }

        private void OnValidate()
        {

        }

        private void OnGUI()
        {
            if (WindowMode == WindowMode.Default)
            {
                DrawContext();
                DrawFeedback();
                DrawActorSelector();
            }
            else if (WindowMode == WindowMode.CreateCondition)
            {
                DrawCreateCondition();
            }
        }

        private void DrawCreateCondition()
        {
            conditionEnum = (ConditionEnum) EditorGUILayout.EnumPopup(conditionEnum, GUIStyle.none);
            if (GUILayout.Button("Create", GUIStyle.none))
            {

                switch (conditionEnum)
                {
                    case ConditionEnum.None:
                        //conditions.Add((Condition) obj);
                        break;
                    case ConditionEnum.Default:
                        //conditions.Add((Condition) obj);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }
            
            if (GUILayout.Button("Cancel", GUIStyle.none))
            {
                WindowMode = WindowMode.Default;
            }
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
            EditorGUI.indentLevel++;
            hasDuration = EditorGUILayout.Toggle("Has Duration", hasDuration);
            if (hasDuration)
                duration = EditorGUILayout.FloatField("Duration", duration);
            DrawCondition();
            EditorGUI.indentLevel--;
        }

        private void DrawCondition()
        {
            showCondition = EditorGUILayout.Foldout(showCondition, "Condition");
            if (!showCondition) 
                return;

            if (GUILayout.Button("+", GUIStyle.none))
            {
                //Fenster -> alle conditions -> add
                WindowMode = WindowMode.CreateCondition;
            }

            /*for (int i = 0; i < conditions.Count; i++)
            {
                conditionType = conditions[i].conditionType;
            }*/

            //conditionType = (ConditionType) EditorGUILayout.EnumPopup("Condition Type", conditionType);
            //conditionValue = EditorGUILayout.FloatField("Value", conditionValue);
        }
    }
}