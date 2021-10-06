using System.Collections.Generic;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.UI.Managers.Scripts
{
    public class CanvasManager : MonoBehaviour, ICanvas
    {

        public static CanvasManager Instance;

        #region SerialzeFields

        

        #endregion

        #region Private Fields

        

        #endregion

        #region Protected Fields

        

        #endregion

        #region Public Fields

        public readonly List<ICanvas> SceneCanvases = new List<ICanvas>();

        #endregion

        #region Public Properties

        

        #endregion

        #region Events

        

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

        #region Private Methods

        

        #endregion

        #region Public Methods
        
        public void ChangeInteractableStatus(bool status)
        {
            foreach (ICanvas canvas in SceneCanvases)
            {
                canvas.ChangeInteractableStatus(status);
            }
        }

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
    }
}
