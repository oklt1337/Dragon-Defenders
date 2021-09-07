using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.UI.Login
{
    public class CanvasManager : MonoBehaviour, ICanvas
    {
        public static CanvasManager Instance;

        private List<ICanvas> _sceneCanvases;

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
            if (_sceneCanvases.Contains(canvas))
                return;
            
            _sceneCanvases.Add(canvas);
        }
        
        public void Unsubscribe(ICanvas canvas)
        {
            if (!_sceneCanvases.Contains(canvas))
                return;
            
            _sceneCanvases.Remove(canvas);
        }

        #endregion

        #region ICanvas

        public void ChangeInteractableStatus(bool status)
        {
            foreach (ICanvas canvas in _sceneCanvases)
            {
                canvas.ChangeInteractableStatus(status);
            }
        }

        #endregion
    }
}
