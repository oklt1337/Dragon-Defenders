
using System;
using UnityEditor;
using UnityEngine;

namespace Utility.CardBuilder.Scripts
{
    public class CreateCardWindow : EditorWindow
    {
        private static CreateCardWindow Instance;
        private string sName;
        public event Action<string> OnCreate;

        public static CreateCardWindow Init(Rect pos)
        {
            Instance = GetWindow<CreateCardWindow>(string.Empty);
            Instance.maxSize =  new Vector2(300,65);
            Instance.minSize =  new Vector2(300,65);
            Instance.position = pos;
            Instance.Show();

            return Instance;
        }

        private void OnGUI()
        {
            DrawFields();
            DrawButtons();
        }

        #region Draw

        private void DrawFields()
        {
            //CardName
            sName = EditorGUILayout.TextField("Name", sName);
        }

        private void DrawButtons()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Create"), GUILayout.Width(60)))
            {
                Create();
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(new GUIContent("Cancel"), GUILayout.Width(60)))
            {
                Cancel();
            }
            GUILayout.EndHorizontal();
        }

        #endregion
        
        private void Create()
        {
            if (string.IsNullOrEmpty(sName)) 
                return;
            
            OnCreate?.Invoke(sName);
            Cancel();
        }
        
        private static void Cancel()
        {
            Instance.Close();
        }
    }
}