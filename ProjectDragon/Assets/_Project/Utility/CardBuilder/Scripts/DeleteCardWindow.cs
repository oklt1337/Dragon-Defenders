
using System;
using UnityEditor;
using UnityEngine;

namespace Utility.CardBuilder.Scripts
{
    public class DeleteCardWindow : EditorWindow
    {
        private static DeleteCardWindow Instance;
        public event Action OnDelete;

        public static DeleteCardWindow Init(Rect pos)
        {
            Instance = GetWindow<DeleteCardWindow>(string.Empty);
            Instance.maxSize =  new Vector2(250,50);
            Instance.minSize =  new Vector2(250,50);
            Instance.position = pos;
            Instance.Show();

            return Instance;
        }

        private void OnGUI()
        {
            GUILayout.Space(5);
            GUILayout.Label("Are you sure you want to delete the card?");
            DrawButtons();
        }

        #region Draw
        
        private void DrawButtons()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Delete"), GUILayout.Width(60)))
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
            OnDelete?.Invoke();
            Cancel();
        }
        
        private static void Cancel()
        {
            Instance.Close();
        }
    }
}