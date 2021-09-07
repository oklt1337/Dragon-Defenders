using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class CanvasManager : MonoBehaviour, ICanvas
    {
        public static CanvasManager Instance;

        public readonly List<ICanvas> SceneCanvases = new List<ICanvas>();

        #region Unity Methods

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        #endregion

        #region Public Methods

        public void Subscribe(ICanvas canvas)
        {
            if (SceneCanvases.Contains(canvas))
                return;
            
            SceneCanvases.Add(canvas);
        }
        
        public void Unsubscribe(ICanvas canvas)
        {
            if (!SceneCanvases.Contains(canvas))
                return;
            
            SceneCanvases.Remove(canvas);
        }

        #endregion

        #region ICanvas

        public void ChangeInteractableStatus(bool status)
        {
            foreach (ICanvas canvas in SceneCanvases)
            {
                canvas.ChangeInteractableStatus(status);
            }
        }

        #endregion
    }
}
