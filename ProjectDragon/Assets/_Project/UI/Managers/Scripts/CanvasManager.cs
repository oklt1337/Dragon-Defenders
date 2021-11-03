using System.Collections.Generic;
using UnityEngine;

namespace UI.Managers.Scripts
{
    public class CanvasManager : MonoBehaviour, ICanvas
    {
        public static CanvasManager Instance;

        #region Public Fields

        public readonly List<ICanvas> SceneCanvases = new List<ICanvas>();

        #endregion

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
        
        /// <summary>
        /// Changes the interactable status of canvas objects
        /// </summary>
        /// <param name="status">bool</param>
        public void ChangeInteractableStatus(bool status)
        {
            foreach (ICanvas canvas in SceneCanvases)
            {
                canvas.ChangeInteractableStatus(status);
            }
        }

        /// <summary>
        /// Subscribes to list of ICanvas.
        /// </summary>
        /// <param name="canvas">ICanvas</param>
        public void Subscribe(ICanvas canvas)
        {
            if (SceneCanvases.Contains(canvas))
                return;
            
            SceneCanvases.Add(canvas);
        }
        
        /// <summary>
        /// Unsubscribes of List of ICanvas
        /// </summary>
        /// <param name="canvas">ICanvas</param>
        public void Unsubscribe(ICanvas canvas)
        {
            if (!SceneCanvases.Contains(canvas))
                return;
            
            SceneCanvases.Remove(canvas);
        }

        #endregion
    }
}
