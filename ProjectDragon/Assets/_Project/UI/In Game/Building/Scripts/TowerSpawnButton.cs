using _Project.GamePlay.GameManager.Scripts;
using _Project.UI.Managers.Scripts;
using _Project.Units.Unit.BaseUnits;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.UI.In_Game.Building.Scripts
{
    public class TowerSpawnButton : MonoBehaviourPun, IPointerClickHandler, IBeginDragHandler, IDragHandler,
        IEndDragHandler, ICanvas
    {
        [SerializeField] private Unit unit;
        [SerializeField] private Camera buildCam;
        
        #region Unity Methods

        private void OnEnable()
        {
            CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
        }

        #endregion
        
        #region Interface Methods

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //TODO: Instantiate transparent unit. (Not Photon Instantiate!)
        }

        public void OnDrag(PointerEventData eventData)
        {
            //TODO: Move transparent unit with courser.
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (GameManager.Instance.PlayerModel.Money < unit.GoldCost)
                return;

            Vector3 screenPos = Input.mousePosition;
            screenPos.z = buildCam.nearClipPlane;
            Ray ray = buildCam.ScreenPointToRay(screenPos);


            Physics.Raycast(ray, out RaycastHit hit);

            if (!hit.collider.CompareTag("Ground")) 
                return;
            
            // Do the spawning when everything works out.
            GameObject tower = PhotonNetwork.Instantiate(string.Concat(Unit.BasePath, unit.UnitName, unit.UnitName), hit.point, Quaternion.identity);

            if (!GameManager.Instance.PlayerModel.ModifyMoney(-unit.GoldCost))
            {
                Destroy(tower);
            }
        }

        #endregion

        public void ChangeInteractableStatus(bool status)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUnitName(Unit newUnit)
        {
            unit = newUnit;
        }
    }
}